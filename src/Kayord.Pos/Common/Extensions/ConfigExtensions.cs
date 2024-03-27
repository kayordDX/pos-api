using Kayord.Pos.Config;

namespace Kayord.Pos.Common.Extensions;

public static class ConfigExtensions
{
    public static IServiceCollection ConfigureConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HaloConfig>(configuration.GetSection("Halo"));
        services.Configure<EmailConfig>(configuration.GetSection("Email"));
        return services;
    }
}