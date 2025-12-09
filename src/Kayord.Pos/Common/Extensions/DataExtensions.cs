using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using TickerQ.EntityFrameworkCore.DbContextFactory;

namespace Kayord.Pos.Common.Extensions;

public static class DataExtensions
{
    public static IServiceCollection ConfigureEF(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            );
            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });
        return services;
    }

    public static async Task ApplyMigrations(this IServiceProvider serviceProvider, IWebHostEnvironment env, CancellationToken ct)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var tickerDb = scope.ServiceProvider.GetRequiredService<TickerQDbContext>();
        if (db.Database.IsNpgsql())
        {
            await db.Database.MigrateAsync(ct);
        }
        if (tickerDb.Database.IsNpgsql())
        {
            await tickerDb.Database.MigrateAsync(ct);
        }

        // Development Seed
        if (env.IsDevelopment() || env.IsEnvironment("Testing"))
        {
            await AppDbSeed.SeedData(db, ct);
        }

        // Production Seed
        await ProdSeed.SeedData(db, ct);
    }
}