using System.Reflection;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kayord.Pos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Adjustment> Adjustment => Set<Adjustment>();
    public DbSet<AdjustmentType> AdjustmentType => Set<AdjustmentType>();
    public DbSet<AdjustmentTypeOutlet> AdjustmentTypeOutlet => Set<AdjustmentTypeOutlet>();
    public DbSet<Business> Business => Set<Business>();
    public DbSet<Clock> Clock => Set<Clock>();
    public DbSet<Outlet> Outlet => Set<Outlet>();
    public DbSet<SalesPeriod> SalesPeriod => Set<SalesPeriod>();
    public DbSet<Section> Section => Set<Section>();
    public DbSet<Table> Table => Set<Table>();
    public DbSet<TableBooking> TableBooking => Set<TableBooking>();
    public DbSet<Menu> Menu => Set<Menu>();
    public DbSet<MenuItem> MenuItem => Set<MenuItem>();
    public DbSet<OrderItem> OrderItem => Set<OrderItem>();
    public DbSet<User> User => Set<User>();
    public DbSet<UserRole> UserRole => Set<UserRole>();
    public DbSet<Role> Role => Set<Role>();
    public DbSet<MenuSection> MenuSection => Set<MenuSection>();
    public DbSet<UserOutlet> UserOutlet => Set<UserOutlet>();
    public DbSet<Option> Option => Set<Option>();
    public DbSet<OptionGroup> OptionGroup => Set<OptionGroup>();
    public DbSet<Extra> Extra => Set<Extra>();
    public DbSet<OrderItemExtra> OrderItemExtra => Set<OrderItemExtra>();
    public DbSet<OrderItemOption> OrderItemOption => Set<OrderItemOption>();
    public DbSet<MenuItemOptionGroup> MenuItemOptionGroup => Set<MenuItemOptionGroup>();
    public DbSet<Division> Division => Set<Division>();
    public DbSet<OrderItemStatus> OrderItemStatus => Set<OrderItemStatus>();
    public DbSet<RoleDivision> RoleDivision => Set<RoleDivision>();
    public DbSet<HaloLog> HaloLog => Set<HaloLog>();
    public DbSet<HaloReference> HaloReference => Set<HaloReference>();
    public DbSet<ExtraGroup> ExtraGroup => Set<ExtraGroup>();
    public DbSet<Payment> Payment => Set<Payment>();
    public DbSet<MenuItemExtraGroup> MenuItemExtraGroup => Set<MenuItemExtraGroup>();
    public DbSet<CashUp> CashUp => Set<CashUp>();
    public DbSet<PaymentType> PaymentType => Set<PaymentType>();
    public DbSet<OutletExtraGroup> OutletExtraGroup => Set<OutletExtraGroup>();
    public DbSet<OrderGroup> OrderGroup => Set<OrderGroup>();
    public DbSet<NotificationUser> NotificationUser => Set<NotificationUser>();
    public DbSet<NotificationLog> NotificationLog => Set<NotificationLog>();
    public DbSet<OrderItemStatusLog> OrderItemStatusLog => Set<OrderItemStatusLog>();
    public DbSet<EmailLog> EmailLog => Set<EmailLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (entityType.IsKeyless)
            {
                continue;
            }

            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableDateTimeConverter);
                }
            }
        }

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}