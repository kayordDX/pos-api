using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
namespace Kayord.Pos.Common.Extensions.Health;

public static class HealthExtensions
{
    public static void ConfigureHealth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddProcessAllocatedMemoryHealthCheck(150).AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);
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