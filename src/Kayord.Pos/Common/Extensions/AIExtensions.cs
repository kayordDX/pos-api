using Kayord.Pos.Services.AI;

namespace Kayord.Pos.Common.Extensions;

public static class AIExtensions
{
    public static IServiceCollection ConfigureAI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<AIService>();
        return services;
    }
}