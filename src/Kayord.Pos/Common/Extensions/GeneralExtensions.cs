using Kayord.Pos.Hubs;
using Kayord.Pos.Services;
using Microsoft.AspNetCore.SignalR;
using QuestPDF.Infrastructure;

namespace Kayord.Pos.Common.Extensions;

public static class GeneralExtensions
{
    public static IServiceCollection ConfigureGeneral(this IServiceCollection services, IConfiguration configuration)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        services.AddHostedService<MigratorHostedService>();
        services.AddSingleton<CurrentUserService>();
        services.AddScoped<UserService>();
        services.AddTransient<IEmailSender, EmailService>();
        services.AddSingleton<IUserIdProvider, UserProvider>();
        services.AddTransient<NotificationService>();

        return services;
    }
}