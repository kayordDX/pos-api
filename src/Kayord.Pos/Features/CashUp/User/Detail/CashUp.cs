namespace Kayord.Pos.Features.CashUp.User.Detail;

using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

public static class CashUp
{
    public static async Task<Response> CashUpProcess(int OutletId, string UserId, AppDbContext _dbContext, CurrentUserService _cu, bool close, int cashUpUserId = 0)
    {

        Response response = new()
        {
            CashUpUserItems = new(),
            IsCashedUp = false,
            IsError = false
        };
        var salesPeriod = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.OutletId == OutletId && x.EndDate == null);
        if (salesPeriod == null)
        {
            response.IsError = true;
            response.Message = "Sales period not found";
            return response;
        }
        if (close)
        {
            bool hasOpenTables = await _dbContext.TableBooking.AnyAsync(x => x.SalesPeriodId == salesPeriod.Id && x.UserId == UserId && (x.CloseDate == null || x.Total == null));
            if (hasOpenTables)
            {
                response.IsError = true;
                response.Message = "User has open tables";
                return response;
            }
        }
        if (cashUpUserId > 0)
        {
            var savedCashUpUser = await _dbContext.CashUpUser.ProjectToDto().FirstOrDefaultAsync(x => x.Id == cashUpUserId);
            response.OpeningBalance = savedCashUpUser?.OpeningBalance ?? 0;
            var savedBalance = (savedCashUpUser?.OpeningBalance ?? 0) - (savedCashUpUser?.ClosingBalance ?? 0);

            response.CashUpUserId = cashUpUserId;
            var items = _dbContext.CashUpUserItem.Where(x => x.CashUpUserId == cashUpUserId).Where(x => x.Value != 0m).ProjectToDto();
            response.CashUpUserItems.AddRange(items);

            response.GrossBalance = Math.Round(response.OpeningBalance + response.CashUpUserItems.Where(x => x.CashUpUserItemType!.AffectsGrossBalance || x.CashUpUserItemType.IsAuto == false).Sum(x => x.Value), 2);
            response.NetBalance = response.GrossBalance;
            response.IsCashedUp = true;

            response.UserId = UserId;
            var savedUser = await _dbContext.User.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == UserId);
            if (savedUser != null)
            {
                response.User = savedUser;
            }
            return response;
        }

        CashUpUserDTO? cashUpUser = await _dbContext.CashUpUser.ProjectToDto().OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.UserId == UserId && x.ClosingBalance == null && x.OutletId == OutletId);
        int userCashUpId = 0;

        if (salesPeriod == null)
        {
            response.IsError = true;
            response.Message = "No active sales period";
            return response;
        }

        if (cashUpUser != null)
        {
            userCashUpId = cashUpUser.Id;
        }
        else
        {
            cashUpUser = await _dbContext.CashUpUser.ProjectToDto().OrderByDescending(x => x.Id).LastOrDefaultAsync(x => x.UserId == UserId && x.ClosingBalance != null && x.OutletId == OutletId);
            if (cashUpUser != null)
            {
                // TODO: Double check with Steff
                CashUpUser u = new()
                {
                    OpeningBalance = cashUpUser.ClosingBalance ?? 0,
                    UserId = UserId,
                    OutletId = cashUpUser.OutletId,
                };
                _dbContext.CashUpUser.Add(u);
                await _dbContext.SaveChangesAsync();
                userCashUpId = u.Id;
                cashUpUser = await _dbContext.CashUpUser.ProjectToDto().FirstOrDefaultAsync(x => x.UserId == UserId && x.ClosingBalance == null && x.OutletId == OutletId);
            }
            else
            {
                // TODO: Double check with Steff
                CashUpUser u = new()
                {
                    OpeningBalance = 0,
                    UserId = UserId,
                    OutletId = OutletId,
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

        // Fixed: Filter on current outlet only
        // List<CashUpUserItemTypeDTO> cashUpUserItemTypes = await _dbContext.CashUpUserItemType.ProjectToDto().ToListAsync();
        List<CashUpUserItemTypeDTO> cashUpUserItemTypesOld = await _dbContext.CashUpUserItemType.ProjectToDto().ToListAsync();
        List<CashUpUserItemTypeDTO> cashUpUserItemTypes = await _dbContext.CashUpUserItemType.GroupJoin(
            _dbContext.AdjustmentTypeOutlet,
            c => c.AdjustmentTypeId,
            a => a.AdjustmentTypeId,
            (c, a) => new { c, a }
        )
        .SelectMany(x => x.a.DefaultIfEmpty(), (x, a) => new { cut = x.c, a })
        .Where(x => (x.a != null && x.a.OutletId == OutletId) || x.cut.AdjustmentTypeId == null)
        .Select(x => x.cut)
        .ProjectToDto()
        .ToListAsync();

        List<PaymentTotal> paymentTotals = new();

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

        var tableBooking = await _dbContext.TableBooking.Where(x => x.UserId == UserId && x.CashUpUserId == null && x.SalesPeriodId == salesPeriod.Id)
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
            // decimal cashPayments = item.Payments?.Where(x => x.TableBookingId == item.Id && !paymentWithLevyIds.Contains(x.PaymentTypeId ?? 0)).Sum(x => x.Amount) ?? 0;
            // decimal cardPayments = item.Payments?.Where(x => x.TableBookingId == item.Id && paymentWithLevyIds.Contains(x.PaymentTypeId ?? 0)).Sum(x => x.Amount) ?? 0;
            decimal totalPayments = item.Payments?.Where(x => x.TableBookingId == item.Id).Sum(x => x.Amount) ?? 0;
            decimal adjustments = item.Adjustments?.Sum(x => x.Amount) ?? 0;

            // var totalLevyOverage = (cardPayments + cashPayments) - tableTotal;
            var totalLevyOverage = totalPayments - tableTotal;
            if (totalLevyOverage > 0)
            {
                List<Payment>? payments = item.Payments?.Where(x => x.TableBookingId == item.Id).ToList();
                if (payments != null)
                {
                    foreach (var tablePayments in payments.OrderBy(x => x.DateReceived))
                    {
                        tableTotal = tableTotal - tablePayments.Amount;
                        if (tableTotal < 0)
                        {
                            var payType = paymentTotals.FirstOrDefault(x => x.PaymentTypeId == tablePayments.PaymentTypeId);
                            if (payType != null)
                            {
                                decimal paymentTipOverage = tableTotal * -1;
                                payType.Tip += paymentTipOverage;

                                bool isLevy = paymentWithLevyIds.Contains(tablePayments.PaymentTypeId ?? 0);
                                if (isLevy)
                                {
                                    payType.Levy += paymentTipOverage * payType.PaymentType.TipLevyPercentage / 100;
                                }
                                else
                                {
                                    payType.Levy += 0;
                                }
                            }
                            tableTotal = 0;
                        }
                    }
                }
            }

            // var levyOverage = cardPayments - tableTotal;
            // if (levyOverage > 0)
            // {

            //     List<Payment>? tablePaymentsWithLevys = item.Payments?.Where(x => x.TableBookingId == item.Id && paymentWithLevyIds.Contains(x.PaymentTypeId ?? 0)).ToList();
            //     if (tablePaymentsWithLevys != null)
            //     {
            //         foreach (var tablePaymentsWithLevy in tablePaymentsWithLevys.OrderByDescending(x => x.PaymentType.TipLevyPercentage))
            //         {
            //             tableTotal = tableTotal - tablePaymentsWithLevy.Amount;
            //             if (tableTotal < 0)
            //             {
            //                 var payType = paymentTotals.FirstOrDefault(x => x.PaymentTypeId == tablePaymentsWithLevy.PaymentTypeId);
            //                 if (payType != null)
            //                 {
            //                     decimal paymentTipOverage = tableTotal * -1;
            //                     payType.Tip += paymentTipOverage;
            //                     payType.Levy += paymentTipOverage * payType.PaymentType.TipLevyPercentage / 100;
            //                 }
            //                 tableTotal = 0;
            //             }
            //         }
            //     }
            // }
            // var cashTipOverage = cashPayments - tableTotal;
            // if (cashTipOverage > 0)
            // {
            //     List<Payment>? tablePaymentsWithOutLevys = item.Payments?.Where(x => x.TableBookingId == item.Id && !paymentWithLevyIds.Contains(x.PaymentTypeId ?? 0)).ToList();
            //     if (tablePaymentsWithOutLevys != null)
            //     {
            //         foreach (var tablePaymentsWithLevy in tablePaymentsWithOutLevys)
            //         {
            //             tableTotal = tableTotal - tablePaymentsWithLevy.Amount;
            //             if (tableTotal < 0)
            //             {
            //                 var payType = paymentTotals.FirstOrDefault(x => x.PaymentTypeId == tablePaymentsWithLevy.PaymentTypeId);
            //                 if (payType != null)
            //                 {
            //                     decimal paymentTipOverage = tableTotal * -1;
            //                     payType.Tip += paymentTipOverage;
            //                     payType.Levy += 0;
            //                 }
            //                 tableTotal = 0;
            //             }
            //         }
            //     }
            // }
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
            var clock = await _dbContext.Clock.FirstOrDefaultAsync(x => x.EndDate == null && x.OutletId == OutletId && x.UserId == UserId);
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
        response.NetBalance = response.GrossBalance;

        if (close)
        {
            if (cashUpUser != null)
            {
                var cashUpUserEntity = await _dbContext.CashUpUser.FindAsync(cashUpUser.Id);
                if (cashUpUserEntity != null)
                {
                    cashUpUserEntity.ClosingBalance = response.NetBalance;
                    cashUpUserEntity.CompleterUserId = _cu.UserId ?? "";
                    cashUpUserEntity.CashUpDate = DateTime.Now;
                    cashUpUserEntity.SalesPeriodId = salesPeriod?.Id ?? 0;

                    // TODO: Check if this needs adjustments. Save Payment, Sales and Tips to DB
                    var tips = paymentTotals.Sum(x => x.Tip);
                    var paymentTotal = tableBooking.Sum(x => x.Payments?.Select(x => x.Amount).Sum()) ?? 0;
                    var sales = tableBooking.Sum(x => x.Total) ?? 0;
                    cashUpUserEntity.Tips = tips;
                    cashUpUserEntity.Sales = sales;
                    cashUpUserEntity.Payments = paymentTotal;
                }
                cashUpUser.ClosingBalance = response.NetBalance;
                cashUpUser.CompleterUserId = _cu.UserId ?? "";
                // TODO: Double check with Steff 
                CashUpUser c = new()
                {
                    UserId = UserId,
                    OpeningBalance = cashUpUser.ClosingBalance ?? 0,
                    OutletId = cashUpUser.OutletId,
                };
                await _dbContext.CashUpUser.AddAsync(c);
                await _dbContext.SaveChangesAsync();
            }
        }

        response.CashUpUserItems = response.CashUpUserItems.OrderBy(x => x.CashUpUserItemType!.Position).Where(x => x.Value != 0m).ToList();
        return response;
    }
}