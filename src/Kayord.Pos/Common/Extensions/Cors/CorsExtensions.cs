using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Kayord.Pos.Common.Extensions.Cors;

public static class CorsExtensions
{
    private static readonly string _allowedOrigins = "KayordOrigins";

    public static void ConfigureCors(this IServiceCollection services, string[] origins)
    {
        string[] origins2 = origins;
        services.AddCors(delegate (CorsOptions options)
        {
            options.AddPolicy(_allowedOrigins, delegate (CorsPolicyBuilder builder)
            {
                builder.WithOrigins(origins2).AllowAnyHeader().AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public static IApplicationBuilder UseCorsKayord(this IApplicationBuilder app)
    {
        app.UseCors(_allowedOrigins);
        return app;
    }
}