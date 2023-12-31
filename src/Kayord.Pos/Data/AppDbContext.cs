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

    public DbSet<Business> Business => Set<Business>();
    public DbSet<Clock> Clock => Set<Clock>();
    public DbSet<Outlet> Outlet => Set<Outlet>();
    public DbSet<SalesPeriod> SalesPeriod => Set<SalesPeriod>();
    public DbSet<Section> Section => Set<Section>();
    public DbSet<Staff> Staff => Set<Staff>();
    public DbSet<Table> Table => Set<Table>();
    public DbSet<TableBooking> TableBooking => Set<TableBooking>();
    public DbSet<Menu> Menu => Set<Menu>();
    public DbSet<MenuItem> MenuItem => Set<MenuItem>();
    public DbSet<TableCashUp> TableCashUp => Set<TableCashUp>();
    public DbSet<OrderItem> OrderItem => Set<OrderItem>();
    public DbSet<TableOrder> TableOrder => Set<TableOrder>();







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