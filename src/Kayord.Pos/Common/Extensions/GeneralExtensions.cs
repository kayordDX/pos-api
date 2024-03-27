using QuestPDF.Infrastructure;

namespace Kayord.Pos.Common.Extensions;

public static class GeneralExtensions
{
    public static IServiceCollection ConfigureGeneral(this IServiceCollection services, IConfiguration configuration)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        return services;
    }
}