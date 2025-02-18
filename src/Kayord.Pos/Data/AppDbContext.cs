using System.Reflection;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kayord.Pos.Data;

public class AppDbContext : DbContext
{
    private readonly CurrentUserService _currentUserService;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        CurrentUserService currentUserService
    )
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Adjustment> Adjustment => Set<Adjustment>();
    public DbSet<AdjustmentType> AdjustmentType => Set<AdjustmentType>();
    public DbSet<AdjustmentTypeOutlet> AdjustmentTypeOutlet => Set<AdjustmentTypeOutlet>();
    public DbSet<Business> Business => Set<Business>();
    public DbSet<CashUp> CashUp => Set<CashUp>();
    public DbSet<CashUpConfig> CashUpConfig => Set<CashUpConfig>();
    public DbSet<CashUpUser> CashUpUser => Set<CashUpUser>();
    public DbSet<CashUpUserItem> CashUpUserItem => Set<CashUpUserItem>();
    public DbSet<CashUpUserItemType> CashUpUserItemType => Set<CashUpUserItemType>();
    public DbSet<Clock> Clock => Set<Clock>();
    public DbSet<Division> Division => Set<Division>();
    public DbSet<DivisionType> DivisionType => Set<DivisionType>();
    public DbSet<EmailLog> EmailLog => Set<EmailLog>();
    public DbSet<Extra> Extra => Set<Extra>();
    public DbSet<ExtraGroup> ExtraGroup => Set<ExtraGroup>();
    public DbSet<HaloConfig> HaloConfig => Set<HaloConfig>();
    public DbSet<HaloLog> HaloLog => Set<HaloLog>();
    public DbSet<HaloReference> HaloReference => Set<HaloReference>();
    public DbSet<Menu> Menu => Set<Menu>();
    public DbSet<MenuItem> MenuItem => Set<MenuItem>();
    public DbSet<MenuItemExtraGroup> MenuItemExtraGroup => Set<MenuItemExtraGroup>();
    public DbSet<MenuItemOptionGroup> MenuItemOptionGroup => Set<MenuItemOptionGroup>();
    public DbSet<MenuItemStock> MenuItemStock => Set<MenuItemStock>();
    public DbSet<MenuSection> MenuSection => Set<MenuSection>();
    public DbSet<NotificationLog> NotificationLog => Set<NotificationLog>();
    public DbSet<NotificationUser> NotificationUser => Set<NotificationUser>();
    public DbSet<Option> Option => Set<Option>();
    public DbSet<OptionGroup> OptionGroup => Set<OptionGroup>();
    public DbSet<OrderGroup> OrderGroup => Set<OrderGroup>();
    public DbSet<OrderItem> OrderItem => Set<OrderItem>();
    public DbSet<OrderItemExtra> OrderItemExtra => Set<OrderItemExtra>();
    public DbSet<OrderItemOption> OrderItemOption => Set<OrderItemOption>();
    public DbSet<OrderItemStatus> OrderItemStatus => Set<OrderItemStatus>();
    public DbSet<Outlet> Outlet => Set<Outlet>();
    public DbSet<OutletExtraGroup> OutletExtraGroup => Set<OutletExtraGroup>();
    public DbSet<OutletPaymentType> OutletPaymentType => Set<OutletPaymentType>();
    public DbSet<Payment> Payment => Set<Payment>();
    public DbSet<PaymentType> PaymentType => Set<PaymentType>();
    public DbSet<Printer> Printer => Set<Printer>();
    public DbSet<Role> Role => Set<Role>();
    public DbSet<RoleDivision> RoleDivision => Set<RoleDivision>();
    public DbSet<SalesPeriod> SalesPeriod => Set<SalesPeriod>();
    public DbSet<Section> Section => Set<Section>();
    public DbSet<Stock> Stock => Set<Stock>();
    public DbSet<StockItem> StockItem => Set<StockItem>();
    public DbSet<StockOrder> StockOrder => Set<StockOrder>();
    public DbSet<StockOrderStatus> StockOrderStatus => Set<StockOrderStatus>();
    public DbSet<StockOrderItem> StockOrderItem => Set<StockOrderItem>();
    public DbSet<StockOrderItemStatus> StockOrderItemStatus => Set<StockOrderItemStatus>();
    public DbSet<Supplier> Supplier => Set<Supplier>();
    public DbSet<Table> Table => Set<Table>();
    public DbSet<TableBooking> TableBooking => Set<TableBooking>();
    public DbSet<User> User => Set<User>();
    public DbSet<Unit> Unit => Set<Unit>();
    public DbSet<UserOutlet> UserOutlet => Set<UserOutlet>();
    public DbSet<UserRoleOutlet> UserRoleOutlet => Set<UserRoleOutlet>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
        );

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

    public override async Task<int> SaveChangesAsync(CancellationToken ct = new CancellationToken())
    {
        foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.Now;
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    break;
            }
        }

        int returnValue = await base.SaveChangesAsync(ct);
        if (returnValue > 0)
        {
            bool saveAudit = false;
            foreach (
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<OrderItem> entry in ChangeTracker.Entries<OrderItem>()
            )
            {
                OrderItemStatusAudit audit =
                    new()
                    {
                        OrderItemId = entry.Entity.OrderItemId,
                        OrderItemStatusId = entry.Entity.OrderItemStatusId,
                        StatusDate = DateTime.UtcNow,
                        UserId = _currentUserService.UserId ?? "",
                    };
                _ = await AddAsync(audit, ct);
                saveAudit = true;
            }
            if (saveAudit)
            {
                await base.SaveChangesAsync(ct);
            }
        }
        return returnValue;
    }
}
