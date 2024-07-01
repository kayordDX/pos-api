namespace Kayord.Pos.Features.CashUp.User.Detail;

using Kayord.Pos.Data;
using Kayord.Pos.Data.Migrations;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

public static class CashUp
{
    public static async Task<Response> CashUpProcess(Request req, AppDbContext _dbContext, CurrentUserService _cu, bool close)
    {
        Response response = new()
        {
            CashUpUserItems = new()
        };
        CashUpUserDTO? cashUpUser = await _dbContext.CashUpUser.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == req.UserId && x.ClosingBalance == null && x.OutletId == req.OutletId);
        int userCashUpId = 0;

        if (cashUpUser != null)
        {
            userCashUpId = cashUpUser.Id;
        }
        else
        {
            cashUpUser = await _dbContext.CashUpUser.ProjectToDto().OrderByDescending(x => x.Id).LastOrDefaultAsync(x => x.UserId == req.UserId && x.ClosingBalance != null && x.OutletId == req.OutletId);
            if (cashUpUser != null)
            {
                CashUpUser u = new()
                {
                    OpeningBalance = cashUpUser.ClosingBalance ?? 0,
                    UserId = req.UserId,
                    OutletId = cashUpUser.OutletId
                };
                _dbContext.CashUpUser.Add(u);
                await _dbContext.SaveChangesAsync();
                userCashUpId = u.Id;
                cashUpUser = await _dbContext.CashUpUser.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == req.UserId && x.ClosingBalance == null && x.OutletId == req.OutletId);
            }
            else
            {
                CashUpUser u = new()
                {
                    OpeningBalance = 0,
                    UserId = req.UserId,
                    OutletId = req.OutletId
                };
                _dbContext.CashUpUser.Add(u);
                await _dbContext.SaveChangesAsync();
                userCashUpId = u.Id;
                cashUpUser = await _dbContext.CashUpUser.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == req.UserId && x.ClosingBalance == null && x.OutletId == req.OutletId);
            }
        }

        if (cashUpUser != null)
        {
            response.OpeningBalance = cashUpUser.OpeningBalance;
        }
        List<CashUpUserItemTypeDTO> cashUpUserItemTypes = await _dbContext.CashUpUserItemType.ProjectToDto().ToListAsync();

        List<PaymentTotal> paymentTotals = new();

        var clock = await _dbContext.Clock.FirstOrDefaultAsync(x => x.EndDate == null && x.OutletId == req.OutletId);
        if (clock == null)
        {
            response.UserId = req.UserId;
            response.User = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == req.UserId) ?? default!;
            return response;
        }
        var outletPayTypes = await _dbContext.OutletPaymentType.Where(x => x.OutletId == req.OutletId).Include(x => x.PaymentType).ToListAsync();
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

        var tableBooking = await _dbContext.TableBooking.Where(x => x.UserId == req.UserId && x.CashUpUserId == null).Include(x => x.Payments).Include(x => x.OrderItems).ToListAsync();

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
                    OutletId = req.OutletId,
                    UserId = req.UserId,
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
                                decimal paymentTipOverage = tipOverage * ptU.TipLevyPercentage / levyTotal;
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
                    OutletId = req.OutletId,
                    UserId = req.UserId,
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
                    OutletId = req.OutletId,
                    Value = pt.Levy,
                    UserId = req.UserId,
                };
                response.CashUpUserItems.Add(riLevy);
            }
        }

        foreach (CashUpUserItemTypeDTO cashItem in cashUpUserItemTypes.Where(x => x.CashUpUserItemRule == Common.Enums.CashUpUserItemRule.Adjustment))
        {
            // Adjustment Type Calcs
        }

        foreach (CashUpUserItemTypeDTO cashItem in cashUpUserItemTypes.Where(x => x.CashUpUserItemRule == Common.Enums.CashUpUserItemRule.Config))
        {
            // Config Type Calcs
        }
        response.GrossBalance = response.OpeningBalance + response.CashUpUserItems.Sum(x => x.Value);
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
        List<CashUpUserItemDTO> existing = await _dbContext.CashUpUserItem.Where(x => x.CashUpUserId == userCashUpId).ProjectToDto().ToListAsync();
        response.NetBalance = response.GrossBalance + existing.Sum(x => x.Value);
        response.UserId = req.UserId;
        response.User = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == req.UserId) ?? default!;
        response.CashUpUserItems.AddRange(existing);
        response.CashUpUserId = userCashUpId;
        if (close)
        {
            if (cashUpUser != null)
            {
                cashUpUser.ClosingBalance = response.NetBalance;
                cashUpUser.CompleterUserId = _cu.UserId ?? "";
                CashUpUser c = new();
                c.UserId = cashUpUser.UserId;
                c.OpeningBalance = cashUpUser.ClosingBalance ?? 0;
                c.OutletId = cashUpUser.OutletId;
                await _dbContext.CashUpUser.AddAsync(c);
                await _dbContext.SaveChangesAsync();
            }
        }
        return response;
    }
}