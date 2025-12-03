using Kayord.Pos.Services.Whatsapp;

namespace Kayord.Pos.Common.Extensions;

public static class WhatsappExtensions
{
    public static IServiceCollection ConfigureWhatsapp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<WhatsappService>(client =>
        {
            client.BaseAddress = new Uri(configuration["Whatsapp:Host"] ?? "http://localhost:3000");
            client.DefaultRequestHeaders.Add("token", configuration["Whatsapp:XApiKey"]);
        });
        return services;
    }
}