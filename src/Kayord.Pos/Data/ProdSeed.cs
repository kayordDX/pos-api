using Kayord.Pos.Entities;
using Kayord.Pos.Features.TableOrder.GetBill;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Data;

public static class ProdSeed
{
    public static async Task SeedData(AppDbContext context, CancellationToken cancellationToken)
    {
        // Seed Totals
        var tableBookings = await context.TableBooking
            .Include(x => x.SalesPeriod)
            .Where(x => x.CloseDate != null && x.Total == null)
            .ToListAsync();

        if (tableBookings.Count > 0)
        {
            foreach (var tableBooking in tableBookings)
            {
                tableBooking.Total = (await Bill.GetTotal(tableBooking.Id, context)).Total;
            }
            await context.SaveChangesAsync(cancellationToken);
        }

        // Roles
        if (!context.Role.Any())
        {
            await context.Role.AddAsync(new Role { Name = "Guest", Description = "Guest", RoleId = 1 });
            await context.Role.AddAsync(new Role { Name = "Waiter", Description = "Waiter", RoleId = 2, isFrontLine = true });
            await context.Role.AddAsync(new Role { Name = "Chef", Description = "Chef", RoleId = 3, isBackOffice = true });
            await context.Role.AddAsync(new Role { Name = "Manager", Description = "Manager", RoleId = 4, isBackOffice = true, isFrontLine = true });
            await context.Role.AddAsync(new Role { Name = "Bar", Description = "Bar", RoleId = 5, isBackOffice = true, isFrontLine = false });
            await context.SaveChangesAsync(cancellationToken);
        }

        // Payment Types
        if (!context.PaymentType.Any())
        {
            await context.PaymentType.AddAsync(new PaymentType { PaymentTypeId = 1, PaymentTypeName = "Halo", DiscountPercentage = 0, TipLevyPercentage = 0 });
            await context.PaymentType.AddAsync(new PaymentType { PaymentTypeId = 2, PaymentTypeName = "Cash", DiscountPercentage = 0, TipLevyPercentage = 0 });
            await context.PaymentType.AddAsync(new PaymentType { PaymentTypeId = 3, PaymentTypeName = "Credit Card", DiscountPercentage = 0, TipLevyPercentage = 2.5m });
            await context.SaveChangesAsync(cancellationToken);
        }

        // Adjustment Types
        if (!context.AdjustmentType.Any())
        {
            await context.AdjustmentType.AddAsync(new AdjustmentType { AdjustmentTypeId = 1, Name = "Other" });
            await context.AdjustmentType.AddAsync(new AdjustmentType { AdjustmentTypeId = 2, Name = "Staff Discount" });
            await context.AdjustmentType.AddAsync(new AdjustmentType { AdjustmentTypeId = 3, Name = "Loyalty" });
            await context.AdjustmentType.AddAsync(new AdjustmentType { AdjustmentTypeId = 4, Name = "Free Meal" });
            await context.SaveChangesAsync(cancellationToken);
        }

        // CashUpConfig
        if (!context.CashUpConfig.Any())
        {
            await context.CashUpConfig.AddAsync(new CashUpConfig() { Id = 1, Value = -20, OutletId = 1, Name = "Breakage Config" });
            await context.SaveChangesAsync(cancellationToken);
        }

        // CashUpUserItemType
        if (!context.CashUpUserItemType.Any())
        {
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Sales Revenue", Id = 1, IsAuto = true, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.SalesRevenue, Position = 1 });

            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Halo Total", Id = 2, IsAuto = true, PaymentTypeId = 1, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.PaymentTotal, Position = 2 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Halo Tips", Id = 3, IsAuto = true, PaymentTypeId = 1, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.PaymentTip, Position = 3 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Halo Levy", Id = 4, IsAuto = true, PaymentTypeId = 1, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.PaymentLevy, Position = 4, AffectsGrossBalance = true });

            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Cash Total", Id = 5, IsAuto = true, PaymentTypeId = 2, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.PaymentTotal, Position = 5 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Cash Tips", Id = 6, IsAuto = true, PaymentTypeId = 2, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.PaymentTip, Position = 6 });

            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Credit Card Total", Id = 7, IsAuto = true, PaymentTypeId = 3, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.PaymentTotal, Position = 7 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Credit Card Tips", Id = 8, IsAuto = true, PaymentTypeId = 3, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.PaymentTip, Position = 8 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Credit Card Levy", Id = 9, IsAuto = true, PaymentTypeId = 3, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.PaymentLevy, Position = 9, AffectsGrossBalance = true });

            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Breakage Fee", Id = 10, IsAuto = true, CashupConfigId = 1, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.Config, Position = 10 });

            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Adjustment Other", Id = 11, IsAuto = true, AdjustmentTypeId = 1, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.Adjustment, Position = 11 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Adjustment Staff Discount", Id = 12, IsAuto = true, AdjustmentTypeId = 2, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.Adjustment, Position = 12 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Adjustment Loyalty", Id = 13, IsAuto = true, AdjustmentTypeId = 3, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.Adjustment, Position = 13 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Adjustment Free Meal", Id = 14, IsAuto = true, AdjustmentTypeId = 4, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.Adjustment, Position = 14 });

            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Deposit", Id = 15, IsAuto = false, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.None, Position = 15 });
            await context.CashUpUserItemType.AddAsync(new CashUpUserItemType() { ItemType = "Withdrawal", Id = 16, IsAuto = false, CashUpUserItemRule = Common.Enums.CashUpUserItemRule.None, Position = 16 });
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}