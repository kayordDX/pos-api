using Kayord.Pos.Services;
using StackExchange.Redis;

namespace Kayord.Pos.Common.Extensions;

public static class PrintExtensions
{
    public static IServiceCollection ConfigurePrint(this IServiceCollection services)
    {
        services.AddSingleton<PrintService>();
        return services;
    }
}