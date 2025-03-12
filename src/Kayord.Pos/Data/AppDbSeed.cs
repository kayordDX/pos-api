using Bogus;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Data;

public static class AppDbSeed
{
    public static async Task SeedData(AppDbContext context, CancellationToken cancellationToken)
    {
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

        if (!context.Division.Any())
        {
            await context.Division.AddAsync(new Division { DivisionId = 1, DivisionName = "Kitchen" });
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!context.Menu.Any() && 1 == 2)
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
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 1, IsBillable = false, Status = "Basket", IsBackOffice = false, IsFrontLine = true, IsCancelled = false, IsComplete = false, IsNotify = false });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 2, Status = "Sent to Kitchen", IsBackOffice = true, IsFrontLine = true, IsCancelled = false, IsComplete = false, IsNotify = false });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 3, Status = "Being Prepared", IsBackOffice = true, IsFrontLine = true, IsComplete = false, IsNotify = false });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 4, IsBillable = false, Status = "Kitchen Cancelled", IsBackOffice = false, IsFrontLine = true, IsComplete = false, IsNotify = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 5, Status = "Ready for Collection", IsBackOffice = false, IsFrontLine = true, IsComplete = false, IsNotify = true });
            await context.OrderItemStatus.AddAsync(new OrderItemStatus { OrderItemStatusId = 6, Status = "Complete", IsBackOffice = false, IsComplete = true, IsFrontLine = false, IsNotify = false });

            await context.SaveChangesAsync(cancellationToken);
            await context.TableBooking.AddAsync(new TableBooking { TableId = 1, SalesPeriodId = 1, BookingName = "Seeded Booking", UserId = "103301258912011927884" });
            await context.TableBooking.AddAsync(new TableBooking { TableId = 2, SalesPeriodId = 1, BookingName = "Seeded Booking 2", UserId = "103301258912011927884" });
            await context.PaymentType.AddAsync(new PaymentType { PaymentTypeId = 1, PaymentTypeName = "HALO" });
            await context.PaymentType.AddAsync(new PaymentType { PaymentTypeId = 2, PaymentTypeName = "CASH/POS" });
            await context.SaveChangesAsync(cancellationToken);

            await context.TableBooking.AddAsync(new TableBooking { TableId = 2, SalesPeriodId = 1, BookingName = "Seeded Booking 2", UserId = "103301258912011927884" });


            await context.SaveChangesAsync(cancellationToken);
            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 1, OrderItemStatusId = 6, TableBookingId = 1 });


            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 2, OrderItemStatusId = 6, TableBookingId = 1 });
            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 2, OrderItemStatusId = 6, TableBookingId = 2 });
            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 2, OrderItemStatusId = 6, TableBookingId = 1 });
            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 2, OrderItemStatusId = 6, TableBookingId = 2 });
            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 2, OrderItemStatusId = 6, TableBookingId = 1 });
            await context.SaveChangesAsync(cancellationToken);
            await context.OrderItemOption.AddAsync(new OrderItemOption() { OrderItemId = 1, OptionId = 1 });
            await context.OrderItemOption.AddAsync(new OrderItemOption() { OrderItemId = 2, OptionId = 2 });
            await context.OrderItemExtra.AddAsync(new OrderItemExtra() { OrderItemId = 3, ExtraId = 2 });
            await context.OrderItemOption.AddAsync(new OrderItemOption() { OrderItemId = 3, OptionId = 2 });
            await context.OrderItemExtra.AddAsync(new OrderItemExtra() { OrderItemId = 2, ExtraId = 2 });
            await context.Payment.AddAsync(new Payment() { Amount = 200m, TableBookingId = 1, UserId = "103301258912011927884", PaymentTypeId = 1 });
            await context.Payment.AddAsync(new Payment() { Amount = 200m, TableBookingId = 1, UserId = "103301258912011927884", PaymentTypeId = 1 });
            await context.Payment.AddAsync(new Payment() { Amount = 200m, TableBookingId = 1, UserId = "103301258912011927884", PaymentTypeId = 2 });
            await context.Payment.AddAsync(new Payment() { Amount = 200m, TableBookingId = 2, UserId = "103301258912011927884", PaymentTypeId = 2 });
            await context.Payment.AddAsync(new Payment() { Amount = 200m, TableBookingId = 2, UserId = "103301258912011927884", PaymentTypeId = 1 });
            await context.SaveChangesAsync(cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            await context.TableBooking.AddAsync(new TableBooking() { BookingDate = DateTime.Now, BookingName = "TestBooking", SalesPeriodId = 1, TableId = 1, UserId = "103301258912011927884" });
            await context.SaveChangesAsync(cancellationToken);

            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 4, Note = "test", TableBookingId = 1, OrderItemStatusId = 2, OrderReceived = DateTime.Now });
            await context.OrderItem.AddAsync(new OrderItem() { MenuItemId = 1, Note = "Another Note", TableBookingId = 1, OrderItemStatusId = 2, OrderReceived = DateTime.Now });
            await context.SaveChangesAsync(cancellationToken);
        }
    }

}