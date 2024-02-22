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
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "UserNotification" RESTART IDENTITY CASCADE;""");
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "ExtraGroup" RESTART IDENTITY CASCADE;""");
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "Extra" RESTART IDENTITY CASCADE;""");
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "MenuItemExtraGroup" RESTART IDENTITY CASCADE;""");



        await context.SalesPeriod.AddAsync(new SalesPeriod { Id = 1, Name = "Test", StartDate = DateTime.Now, OutletId = 1 });
        if (!context.Menu.Any())
        {

            await context.Menu.AddAsync(new Menu { Name = "Main", OutletId = 1 });
            await context.Menu.AddAsync(new Menu { Name = "Wine List", OutletId = 1 });

            await context.MenuSection.AddAsync(new MenuSection { Name = "Breakfast", MenuId = 1, PositionId = 1 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Fillings & Extras", MenuId = 1, PositionId = 2 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Flapjacks", MenuId = 1, PositionId = 3 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Hot Beverages", MenuId = 1, PositionId = 4 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Cold Beverages", MenuId = 1, PositionId = 5 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "100% Natural Smoothies", MenuId = 1, PositionId = 6 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Natural Cold Pressed Juices", MenuId = 1, ParentId = 5, PositionId = 7 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Toasted Sandwiches", MenuId = 1, PositionId = 8 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Tramezzini", MenuId = 1, PositionId = 9 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Open Sandwiches", MenuId = 1, PositionId = 10 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Wrap", MenuId = 1, PositionId = 11 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Clean Eats", MenuId = 1, PositionId = 12 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Burger", MenuId = 1, PositionId = 13 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Salad", MenuId = 1, PositionId = 14 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Starters", MenuId = 1, PositionId = 15 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Pasta", MenuId = 1, PositionId = 16 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Pizza", MenuId = 1, PositionId = 17 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Grill & Seafood", MenuId = 1, PositionId = 18 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Curry", MenuId = 1, PositionId = 19 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Deserts", MenuId = 1, PositionId = 20 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "White Wine", MenuId = 2, PositionId = 21 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Red Wine", MenuId = 2, PositionId = 22 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Fortified Wine", MenuId = 2, PositionId = 23 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Dry", MenuId = 2, PositionId = 24, ParentId = 21 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Sweet", MenuId = 2, PositionId = 25, ParentId = 21 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Semi-Sweet", MenuId = 2, PositionId = 26, ParentId = 21 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Dry", MenuId = 2, PositionId = 24, ParentId = 22 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Sweet", MenuId = 2, PositionId = 25, ParentId = 22 });
            await context.MenuSection.AddAsync(new MenuSection { Name = "Semi-Sweet", MenuId = 2, PositionId = 26, ParentId = 22 });


            await context.SaveChangesAsync(cancellationToken);
            await context.MenuItem.AddAsync(new MenuItem { Name = "Coffee", MenuSectionId = 4, Price = (decimal)21.80, Description = "This is more than just a cup of coffee, it's a moment of pure bliss", DivisionId = 1 });
            await context.MenuItem.AddAsync(new MenuItem { Name = "Cappucino", MenuSectionId = 4, Price = (decimal)28.90, Description = "Indulge in the Italian Dream: Our Delectable Cappuccino", DivisionId = 1 });
            await context.MenuItem.AddAsync(new MenuItem { Name = "Coke", MenuSectionId = 5, Price = (decimal)18.80, Description = "The world's most recognizable beverage", DivisionId = 1 });
            await context.MenuItem.AddAsync(new MenuItem { Name = "Spier - Chardonnay Pinot Noir ", MenuSectionId = 5, Price = (decimal)18.80, Description = "The world's most recognizable beverage", DivisionId = 1 });



            await context.ExtraGroup.AddAsync(new ExtraGroup { Name = "Pizza Extras" });
            await context.SaveChangesAsync(cancellationToken);

            await context.Extra.AddAsync(new Extra { Name = "Cheese", Price = 25.80M, ExtraGroupId = 1 });
            await context.Extra.AddAsync(new Extra { Name = "Bacon", Price = 25.80M, ExtraGroupId = 1 });
            await context.Extra.AddAsync(new Extra { Name = "Avo", Price = 25.80M, ExtraGroupId = 1 });
            await context.Extra.AddAsync(new Extra { Name = "Anchovies", Price = 25.80M, ExtraGroupId = 1 });
            await context.Extra.AddAsync(new Extra { Name = "Pilchards", Price = 25.80M, ExtraGroupId = 1 });
            await context.Extra.AddAsync(new Extra { Name = "Pineapple", Price = -25.80M, ExtraGroupId = 1 });

            await context.OptionGroup.AddAsync(new OptionGroup { Name = "Pizza Extras", MinSelections = 0, MaxSelections = 5 });
            await context.OptionGroup.AddAsync(new OptionGroup { Name = "Pizza Base", MinSelections = 1, MaxSelections = 1 });
            await context.SaveChangesAsync(cancellationToken);
            await context.Option.AddAsync(new Option { Name = "Avo", Price = 5, OptionGroupId = 1 });
            await context.Option.AddAsync(new Option { Name = "Double Cheese", Price = 5, OptionGroupId = 1 });
            await context.Option.AddAsync(new Option { Name = "Triple Cheese", Price = 5, OptionGroupId = 1 });
            await context.Option.AddAsync(new Option { Name = "Gluten Free", Price = 20, OptionGroupId = 2 });
            await context.Option.AddAsync(new Option { Name = "Thin", Price = 0, OptionGroupId = 2 });
            await context.Option.AddAsync(new Option { Name = "Thick", Price = 0, OptionGroupId = 2 });
            await context.MenuItem.AddAsync(new MenuItem { Name = "Bacon & Feta", MenuSectionId = 17, Price = (decimal)99.50, DivisionId = 1, });
            await context.MenuItem.AddAsync(new MenuItem { Name = "Chicken Mayo", MenuSectionId = 17, Price = (decimal)110.00, DivisionId = 1 });
            await context.MenuItemExtraGroup.AddAsync(new MenuItemExtraGroup { MenuItemId = 5, ExtraGroupId = 1 });

            await context.MenuItemOptionGroup.AddAsync(new MenuItemOptionGroup { MenuItemId = 4, OptionGroupId = 1 });
            await context.MenuItemOptionGroup.AddAsync(new MenuItemOptionGroup { MenuItemId = 4, OptionGroupId = 2 });
            await context.MenuItemOptionGroup.AddAsync(new MenuItemOptionGroup { MenuItemId = 5, OptionGroupId = 1 });
            await context.MenuItemOptionGroup.AddAsync(new MenuItemOptionGroup { MenuItemId = 5, OptionGroupId = 2 });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 1, Status = "Basket", isBackOffice = false, isFrontLine = true, isCancelled = false, isComplete = false, Notify = false });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 2, Status = "Sent to Kitchen", isBackOffice = true, isFrontLine = true, isCancelled = false, isComplete = false, Notify = false });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 3, Status = "Being Prepared", isBackOffice = true, isFrontLine = true, isComplete = false, Notify = false });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 4, Status = "Kitchen Cancelled", isBackOffice = false, isFrontLine = true, isComplete = false, Notify = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 5, Status = "Ready for Collection", isBackOffice = false, isFrontLine = true, isComplete = false, Notify = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 6, Status = "Complete", isBackOffice = false, isComplete = true, isFrontLine = false, Notify = false });
            await context.SaveChangesAsync(cancellationToken);


            await context.TableBooking.AddAsync(new TableBooking() { BookingDate = DateTime.Now, BookingName = "TestBooking", SalesPeriodId = 1, TableId = 1, UserId = "103301258912011927884" });
            await context.SaveChangesAsync(cancellationToken);

            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 4, Note = "test", TableBookingId = 1, OrderItemStatusId = 2, OrderReceived = DateTime.Now });
            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 1, Note = "Another Note", TableBookingId = 1, OrderItemStatusId = 2, OrderReceived = DateTime.Now });
            await context.SaveChangesAsync(cancellationToken);
        }

    }
}