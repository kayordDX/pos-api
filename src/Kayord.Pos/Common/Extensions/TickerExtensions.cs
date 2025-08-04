using Kayord.Pos.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
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
            opt.SetMaxConcurrency(1);
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