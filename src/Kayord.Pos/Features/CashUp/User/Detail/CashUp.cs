namespace Kayord.Pos.Features.CashUp.User.Detail;

using Google.Apis.Util;
using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

public static class CashUp
{
    public static async Task<Response> CashUpProcess(int OutletId, string UserId, AppDbContext _dbContext, CurrentUserService _cu, bool close)
    {
        Response response = new()
        {
            CashUpUserItems = new()
        };
        CashUpUserDTO? cashUpUser = await _dbContext.CashUpUser.ProjectToDto().OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.UserId == UserId && x.ClosingBalance == null && x.OutletId == OutletId);
        int userCashUpId = 0;

        if (cashUpUser != null)
        {
            userCashUpId = cashUpUser.Id;
        }
        else
        {
            cashUpUser = await _dbContext.CashUpUser.ProjectToDto().OrderByDescending(x => x.Id).LastOrDefaultAsync(x => x.UserId == UserId && x.ClosingBalance != null && x.OutletId == OutletId);
            if (cashUpUser != null)
            {
                CashUpUser u = new()
                {
                    OpeningBalance = cashUpUser.ClosingBalance ?? 0,
                    UserId = UserId,
                    OutletId = cashUpUser.OutletId
                };
                _dbContext.CashUpUser.Add(u);
                await _dbContext.SaveChangesAsync();
                userCashUpId = u.Id;
                cashUpUser = await _dbContext.CashUpUser.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == UserId && x.ClosingBalance == null && x.OutletId == OutletId);
            }
            else
            {
                CashUpUser u = new()
                {
                    OpeningBalance = 0,
                    UserId = UserId,
                    OutletId = OutletId
                };
                _dbContext.CashUpUser.Add(u);
                await _dbContext.SaveChangesAsync();
                userCashUpId = u.Id;
                cashUpUser = await _dbContext.CashUpUser.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == UserId && x.ClosingBalance == null && x.OutletId == OutletId);
            }
        }

        if (cashUpUser != null)
        {
            response.OpeningBalance = cashUpUser.OpeningBalance;
        }
        List<CashUpUserItemTypeDTO> cashUpUserItemTypes = await _dbContext.CashUpUserItemType.ProjectToDto().ToListAsync();

        List<PaymentTotal> paymentTotals = new();

        var clock = await _dbContext.Clock.FirstOrDefaultAsync(x => x.EndDate == null && x.OutletId == OutletId);
        if (clock == null)
        {
            response.UserId = UserId;
            response.User = await _dbContext.User.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == UserId) ?? default!;
            return response;
        }
        var outletPayTypes = await _dbContext.OutletPaymentType.Where(x => x.OutletId == OutletId).Include(x => x.PaymentType).ToListAsync();
        var outletPayTypeIds = outletPayTypes.Select(x => x.PaymentTypeId).ToList();

        var payTypes = await _dbContext.PaymentType.Where(x => outletPayTypeIds.Contains(x.PaymentTypeId)).ProjectToDto().ToListAsync();

        foreach (var payType in payTypes)
        {
            PaymentTotal paymentTotal = new()
            {
                PaymentTypeId = payType.PaymentTypeId,
                PaymentType = payType,
                Total = 0,
                Levy = 0,
                Tip = 0
            };
            paymentTotals.Add(paymentTotal);
        }

        var tableBooking = await _dbContext.TableBooking.Where(x => x.UserId == UserId && x.CashUpUserId == null)
            .Include(x => x.Payments)
            .Include(x => x.Adjustments!)
                .ThenInclude(x => x.AdjustmentType)
            .Include(x => x.OrderItems)
            .ToListAsync();

        List<int> paymentWithLevyIds = outletPayTypes.Where(x => x.PaymentType.TipLevyPercentage != 0m).Select(rd => rd.PaymentTypeId).ToList();

        // Process PaymentTotal
        foreach (CashUpUserItemTypeDTO cashItem in cashUpUserItemTypes.Where(x => x.CashUpUserItemRule == Common.Enums.CashUpUserItemRule.PaymentTotal))
        {
            foreach (var payType in payTypes.Where(x => x.PaymentTypeId == cashItem.PaymentTypeId))
            {
                decimal total = 0m;
                foreach (var item in tableBooking)
                {
                    total += item.Payments?.Where(x => x.PaymentTypeId == payType.PaymentTypeId).Sum(x => x.Amount) ?? 0;
                }

                CashUpUserItemDTO riTotal = new()
                {
                    CashUpUserItemType = cashItem,
                    CashUpUserItemTypeId = cashItem.Id,
                    CashUpUserId = userCashUpId,
                    OutletId = OutletId,
                    UserId = UserId,
                    Value = total
                };
                response.CashUpUserItems.Add(riTotal);
            }
        }

        // Process PaymentTip and PaymentLevy
        foreach (var item in tableBooking)
        {
            decimal tableTotal = item.Total ?? 0;
            decimal cashPayments = item.Payments?.Where(x => x.TableBookingId == item.Id && !paymentWithLevyIds.Contains(x.PaymentTypeId ?? 0)).Sum(x => x.Amount) ?? 0;
            decimal cardPayments = item.Payments?.Where(x => x.TableBookingId == item.Id && paymentWithLevyIds.Contains(x.PaymentTypeId ?? 0)).Sum(x => x.Amount) ?? 0;
            decimal adjustments = item.Adjustments?.Sum(x => x.Amount) ?? 0;

            decimal tipOverage = cashPayments + cardPayments - tableTotal;

            if (tipOverage > 0)
            {
                decimal cashTipCover = Math.Min(tipOverage, cashPayments);
                tipOverage -= cashTipCover;

                if (tipOverage > 0)
                {
                    List<PaymentTypeDTO> PTUsed = payTypes.Where(x => paymentWithLevyIds.Contains(x.PaymentTypeId)).ToList();
                    decimal levyTotal = PTUsed.Sum(x => x.TipLevyPercentage);
                    decimal levyCount = PTUsed.Count;

                    if (levyCount != 0)
                    {
                        foreach (var ptU in PTUsed)
                        {
                            var payType = paymentTotals.FirstOrDefault(x => x.PaymentTypeId == ptU.PaymentTypeId);
                            if (payType != null)
                            {
                                decimal paymentTipOverage = tipOverage; // * ptU.TipLevyPercentage / levyTotal;
                                payType.Tip += paymentTipOverage;
                                payType.Levy += paymentTipOverage * ptU.TipLevyPercentage / 100; // Corrected Levy calculation
                            }
                        }
                    }
                }
            }
        }

        // Create response items for PaymentTip and PaymentLevy
        foreach (var pt in paymentTotals)
        {
            CashUpUserItemTypeDTO? payCashTip = cashUpUserItemTypes.FirstOrDefault(x => x.PaymentTypeId == pt.PaymentTypeId && x.CashUpUserItemRule == Common.Enums.CashUpUserItemRule.PaymentTip);
            if (payCashTip != null)
            {
                CashUpUserItemDTO riTip = new()
                {
                    CashUpUserItemType = payCashTip,
                    CashUpUserItemTypeId = payCashTip.Id,
                    CashUpUserId = userCashUpId,
                    OutletId = OutletId,
                    UserId = UserId,
                    Value = pt.Tip
                };
                response.CashUpUserItems.Add(riTip);
            }

            CashUpUserItemTypeDTO? payCashLevy = cashUpUserItemTypes.FirstOrDefault(x => x.PaymentTypeId == pt.PaymentTypeId && x.CashUpUserItemRule == Common.Enums.CashUpUserItemRule.PaymentLevy);
            if (payCashLevy != null)
            {
                CashUpUserItemDTO riLevy = new()
                {
                    CashUpUserItemType = payCashLevy,
                    CashUpUserItemTypeId = payCashLevy.Id,
                    CashUpUserId = userCashUpId,
                    OutletId = OutletId,
                    Value = pt.Levy * -1,
                    UserId = UserId,
                };
                response.CashUpUserItems.Add(riLevy);
            }
        }
        foreach (CashUpUserItemTypeDTO cashItem in cashUpUserItemTypes.Where(x => x.CashUpUserItemRule == Common.Enums.CashUpUserItemRule.SalesRevenue))
        {
            decimal totalSales = tableBooking.Where(x => x.CloseDate != null).Sum(x => x.Total ?? 0);
            CashUpUserItemDTO salesRev = new()
            {
                CashUpUserItemType = cashItem,
                CashUpUserItemTypeId = cashItem.Id,
                CashUpUserId = userCashUpId,
                OutletId = OutletId,
                Value = totalSales,
                UserId = UserId,
            };
            response.CashUpUserItems.Add(salesRev);
        }
        var cashUpUserItemTypesAdjustment = cashUpUserItemTypes.Where(x => x.CashUpUserItemRule.Equals(Common.Enums.CashUpUserItemRule.Adjustment)).ToList();
        foreach (CashUpUserItemTypeDTO cashItem in cashUpUserItemTypesAdjustment)
        {
            decimal adjustTotal = 0;
            foreach (var tb in tableBooking)
            {
                if (tb.Adjustments != null && cashItem.AdjustmentTypeId != null)
                {
                    var adjustments = tb.Adjustments.Where(x => x.AdjustmentTypeId == cashItem.AdjustmentTypeId).ToList();
                    if (adjustments != null)
                    {
                        adjustTotal += adjustments.Sum(x => x.Amount);
                    }
                }
            }
            CashUpUserItemDTO adjust = new()
            {
                CashUpUserItemType = cashItem,
                CashUpUserItemTypeId = cashItem.Id,
                CashUpUserId = userCashUpId,
                OutletId = OutletId,
                Value = adjustTotal,
                UserId = UserId,
            };
            response.CashUpUserItems.Add(adjust);
        }

        foreach (CashUpUserItemTypeDTO cashItem in cashUpUserItemTypes.Where(x => x.CashUpUserItemRule == Common.Enums.CashUpUserItemRule.Config))
        {

            CashUpConfig? configItem = await _dbContext.CashUpConfig.FirstOrDefaultAsync(x => x.Id == cashItem.CashupConfigId && x.OutletId == OutletId);
            if (configItem != null)
            {
                CashUpUserItemDTO configType = new()
                {
                    CashUpUserItemType = cashItem,
                    CashUpUserItemTypeId = cashItem.Id,
                    CashUpUserId = userCashUpId,
                    OutletId = OutletId,
                    Value = configItem.Value,
                    UserId = UserId,
                };
                response.CashUpUserItems.Add(configType);
            }
        }

        response.GrossBalance = Math.Round(response.OpeningBalance + response.CashUpUserItems.Where(x => x.CashUpUserItemType!.AffectsGrossBalance).Sum(x => x.Value), 2);

        if (close)
        {
            foreach (CashUpUserItemDTO ci in response.CashUpUserItems)
            {
                CashUpUserItem sci = new()
                {
                    CashUpUserId = ci.CashUpUserId,
                    CashUpUserItemTypeId = ci.CashUpUserItemTypeId,
                    Value = ci.Value,
                    OutletId = ci.OutletId,
                    UserId = ci.UserId
                };
                await _dbContext.CashUpUserItem.AddAsync(sci);
            }
            if (cashUpUser != null)
            {
                foreach (TableBooking tb in tableBooking)
                {
                    tb.CashUpUserId = userCashUpId;
                }
            }
            if (clock != null)
            {
                clock.EndDate = DateTime.Now;
            }

            await _dbContext.SaveChangesAsync();
            response.CashUpUserItems = new List<CashUpUserItemDTO>();
        }
        List<CashUpUserItemDTO> existing = await _dbContext.CashUpUserItem.Include(x => x.CashUpUserItemType).Where(x => x.CashUpUserId == userCashUpId).ProjectToDto().ToListAsync();

        response.UserId = UserId;
        response.User = await _dbContext.User.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == UserId) ?? default!;
        response.CashUpUserItems.AddRange(existing);
        response.CashUpUserId = userCashUpId;

        response.GrossBalance = Math.Round(response.OpeningBalance + response.CashUpUserItems.Where(x => x.CashUpUserItemType!.AffectsGrossBalance || x.CashUpUserItemType.IsAuto == false).Sum(x => x.Value), 2);
        response.NetBalance = Math.Round(response.OpeningBalance + response.GrossBalance, 2);

        if (close)
        {
            if (cashUpUser != null)
            {
                var cashUpUserEntity = await _dbContext.CashUpUser.FindAsync(cashUpUser.Id);
                if (cashUpUserEntity != null)
                {
                    cashUpUserEntity.ClosingBalance = response.NetBalance;
                    cashUpUserEntity.CompleterUserId = _cu.UserId ?? "";
                }
                cashUpUser.ClosingBalance = response.NetBalance;
                cashUpUser.CompleterUserId = _cu.UserId ?? "";
                CashUpUser c = new()
                {
                    UserId = UserId,
                    OpeningBalance = cashUpUser.ClosingBalance ?? 0,
                    OutletId = cashUpUser.OutletId
                };
                await _dbContext.CashUpUser.AddAsync(c);
                await _dbContext.SaveChangesAsync();
            }
        }

        response.CashUpUserItems = response.CashUpUserItems.OrderBy(x => x.CashUpUserItemType!.Position).ToList();
        return response;
    }
}