using HealthChecks.UI.Client;
using Kayord.Pos.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
namespace Kayord.Pos.Common.Extensions.Health;

public static class HealthExtensions
{
    public static void ConfigureHealth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddProcessAllocatedMemoryHealthCheck(2750)
            .AddDbContextCheck<AppDbContext>()
            .AddRedis(configuration.GetConnectionString("Redis")!);
    }

    public static IApplicationBuilder UseHealth(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        return app;
    }
}