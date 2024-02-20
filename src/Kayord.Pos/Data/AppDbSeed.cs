using System.Reflection;
using Bogus;
using Bogus.Extensions.UnitedStates;
using Humanizer;
using Kayord.Pos.Common.Enums;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kayord.Pos.Data;

public static class AppDbSeed
{
    public static async Task SeedData(AppDbContext context, CancellationToken cancellationToken)
    {
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "Business" RESTART IDENTITY CASCADE;""");
        if (!context.Business.Any())
        {
            var table = new Faker<Table>()
                .RuleFor(o => o.Name, f => f.Name.JobArea())
                .RuleFor(o => o.Capacity, f => f.Random.Int(1, 14));
            var section = new Faker<Section>()
                .RuleFor(o => o.Name, f => f.Name.FirstName())
                .RuleFor(o => o.Tables, _ => table.GenerateBetween(5, 7));
            var outlet = new Faker<Outlet>()
                .RuleFor(o => o.Name, f => f.Company.CompanyName())
                .RuleFor(o => o.Sections, _ => section.GenerateBetween(3, 5));
            var business = new Faker<Business>()
                .RuleFor(o => o.Name, f => f.Company.CompanyName())
                .RuleFor(o => o.Outlets, _ => outlet.GenerateBetween(2, 5));


            var results = business.GenerateBetween(1, 1);
            await context.Business.AddRangeAsync(results);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!context.Role.Any())
        {
            await context.Role.AddAsync(new Role { Name = "Guest", Description = "Guest", RoleId = 1 });
            await context.Role.AddAsync(new Role { Name = "Waiter", Description = "Waiter", RoleId = 2 });
            await context.Role.AddAsync(new Role { Name = "Chef", Description = "Chef", RoleId = 3 });
            await context.Role.AddAsync(new Role { Name = "Manager", Description = "Manager", RoleId = 4 });
            await context.SaveChangesAsync(cancellationToken);
        }
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "Menu" RESTART IDENTITY CASCADE;""");
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "MenuSection" RESTART IDENTITY CASCADE;""");
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "Option" RESTART IDENTITY CASCADE;""");
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "MenuItemOptionGroup" RESTART IDENTITY CASCADE;""");
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "OptionGroup" RESTART IDENTITY CASCADE;""");
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "OrderItemStatus" RESTART IDENTITY CASCADE;""");


        await context.SalesPeriod.AddAsync(new SalesPeriod { Id = 1, Name = "Test", StartDate = DateTime.Now, OutletId = 1 });
        if (!context.Menu.Any())
        {

            await context.Menu.AddAsync(new Menu { Name = "Main", OutletId = 1 });
            await context.Menu.AddAsync(new Menu { Name = "Wine List", OutletId = 1 });

            await context.MenuSection.AddAsync(new MenuSection { Name = "Burgers", MenuId = 1 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Drinks", MenuId = 1 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Cocktails", MenuId = 1, ParentId = 2 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Soft Drinks", MenuId = 1, ParentId = 2 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Hot Beverage", MenuId = 1, ParentId = 2 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Coffee/Cappucino", MenuId = 1, ParentId = 5 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Desert", MenuId = 1 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Pizza", MenuId = 1 });

            await context.MenuSection.AddAsync(
                new MenuSection
                {
                    Name = "White Wine",
                    MenuId = 2,
                    MenuItems = new List<MenuItem>() {
                    new MenuItem {
                        Name = "Sauvignon blanc", Price = (decimal)75.74 }
                    }
                });
            await context.MenuSection.AddAsync(
                new MenuSection
                {
                    Name = "Red Wine",
                    MenuId = 2,
                    MenuItems = new List<MenuItem>() {
                    new MenuItem {
                        Name = "Chardonnay KWV", Price = 98 }
                    }
                });

            await context.SaveChangesAsync(cancellationToken);
            await context.MenuItem.AddAsync(new MenuItem { Name = "Coffee", MenuSectionId = 6, Price = (decimal)21.80, Description = "This is more than just a cup of coffee, it's a moment of pure bliss" });
            await context.MenuItem.AddAsync(new MenuItem { Name = "Cappucino", MenuSectionId = 6, Price = (decimal)28.90, Description = "Indulge in the Italian Dream: Our Delectable Cappuccino" });
            await context.MenuItem.AddAsync(new MenuItem { Name = "Coke", MenuSectionId = 4, Price = (decimal)18.80, Description = "The world's most recognizable beverage" });

            await context.MenuItem.AddAsync(new MenuItem { Name = "Bacon & Feta", MenuSectionId = 8, Price = (decimal)99.50 });
            await context.MenuItem.AddAsync(new MenuItem { Name = "Chicken Mayo", MenuSectionId = 8, Price = (decimal)110.00 });

            await context.OptionGroup.AddAsync(new OptionGroup { Name = "Pizza Extras", MinSelections = 0, MaxSelections = 5 });
            await context.OptionGroup.AddAsync(new OptionGroup { Name = "Pizza Base", MinSelections = 1, MaxSelections = 1 });
            await context.SaveChangesAsync(cancellationToken);
            await context.Option.AddAsync(new Option { Name = "Avo", Price = 5, OptionGroupId = 1 });
            await context.Option.AddAsync(new Option { Name = "Double Cheese", Price = 5, OptionGroupId = 1 });
            await context.Option.AddAsync(new Option { Name = "Triple Cheese", Price = 5, OptionGroupId = 1 });
            await context.Option.AddAsync(new Option { Name = "Gluten Free", Price = 20, OptionGroupId = 2 });
            await context.Option.AddAsync(new Option { Name = "Thin", Price = 0, OptionGroupId = 2 });
            await context.Option.AddAsync(new Option { Name = "Thick", Price = 0, OptionGroupId = 2 });

            await context.MenuItemOptionGroup.AddAsync(new MenuItemOptionGroup { MenuItemId = 4, OptionGroupId = 1 });
            await context.MenuItemOptionGroup.AddAsync(new MenuItemOptionGroup { MenuItemId = 4, OptionGroupId = 2 });
            await context.MenuItemOptionGroup.AddAsync(new MenuItemOptionGroup { MenuItemId = 5, OptionGroupId = 1 });
            await context.MenuItemOptionGroup.AddAsync(new MenuItemOptionGroup { MenuItemId = 5, OptionGroupId = 2 });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 1, Status ="Basket",isKitchen = false,isFrontLine = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 2, Status ="Sent to Kitchen",isKitchen = true,isFrontLine = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 3, Status ="Being Prepared",isKitchen = true,isFrontLine = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 4, Status ="Kitchen Cancelled",isKitchen = false,isFrontLine = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 5, Status ="Ready for Collection",isKitchen = false,isFrontLine = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 6, Status ="Complete",isKitchen = false,isFrontLine = true });





            await context.SaveChangesAsync(cancellationToken);


        }

    }
}