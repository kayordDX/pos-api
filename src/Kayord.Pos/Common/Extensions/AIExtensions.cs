using Kayord.Pos.Config;
using Microsoft.SemanticKernel;

namespace Kayord.Pos.Common.Extensions;

public static class AIExtensions
{
    public static IServiceCollection ConfigureAI(this IServiceCollection services, IConfiguration configuration)
    {
        var appConfig = configuration.GetSection("App").Get<AppConfig>();

        services.AddGoogleAIGeminiChatCompletion(
            modelId: appConfig?.GeminiModel ?? "gemini-2.5-flash-lite",
            apiKey: appConfig?.GeminiKey ?? "no-key"
        );

        services.AddTransient((serviceProvider) =>
        {
            return new Kernel(serviceProvider);
        });

        return services;
    }
}