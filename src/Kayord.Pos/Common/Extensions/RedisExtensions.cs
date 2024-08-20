using Kayord.Pos.Services;
using StackExchange.Redis;

namespace Kayord.Pos.Common.Extensions;

public static class RedisExtensions
{
    public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(o =>
        {
            o.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddSingleton<RedisClient>();
        return services;
    }
}