using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.Customizer;
using TickerQ.EntityFrameworkCore.DbContextFactory;
using TickerQ.EntityFrameworkCore.DependencyInjection;

namespace Kayord.Pos.Common.Extensions;

public static class TickerExtensions
{
    public static void ConfigureTickerQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTickerQ(opt =>
        {
            // opt.SetInstanceIdentifier("pos-api");
            // opt.AddOperationalStore<AppDbContext>(o =>
            // {
            //     o.UseModelCustomizerForMigrations();
            // });
            // opt.AddDashboard(o =>
            // {
            //     o.EnableBasicAuth = true;
            // });

            opt.AddOperationalStore(o =>
            {
                o.UseTickerQDbContext<TickerQDbContext>(options =>
                {
                    options.UseSnakeCaseNamingConvention();
                    options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                    );
                });
                // o.UseApplicationDbContext<AppDbContext>(ConfigurationType.UseModelCustomizer);
            });

            opt.AddDashboard(o =>
            {
                o.WithBasicAuth(configuration["TickerQBasicAuth:Username"], configuration["TickerQBasicAuth:Password"]);
            });
        });
    }
}