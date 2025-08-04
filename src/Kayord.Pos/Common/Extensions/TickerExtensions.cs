using Kayord.Pos.Data;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DependencyInjection;

namespace Kayord.Pos.Common.Extensions;

public static class TickerExtensions
{
    public static void ConfigureTickerQ(this IServiceCollection services)
    {
        services.AddTickerQ(opt =>
        {
            opt.SetInstanceIdentifier("Main");
            opt.AddOperationalStore<AppDbContext>(o =>
            {
                o.UseModelCustomizerForMigrations();
                o.CancelMissedTickersOnApplicationRestart();
            });
            opt.AddDashboard();
            opt.AddDashboardBasicAuth();
        });
    }
}