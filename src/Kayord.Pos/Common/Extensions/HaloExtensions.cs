using Kayord.Pos.Features.Pay;

namespace Kayord.Pos.Common.Extensions;

public static class HaloExtensions
{
    public static IServiceCollection ConfigureHalo(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<HaloService>(client =>
        {
            client.BaseAddress = new Uri($"https://kernelserver.{configuration["Halo:Environment"]}.haloplus.io/{configuration["Halo:Version"]}/");
            // client.DefaultRequestHeaders.Add("x-api-key", configuration["Halo:XApiKey"]);
        });
        return services;
    }
}