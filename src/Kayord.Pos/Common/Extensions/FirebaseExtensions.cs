
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;

namespace Kayord.Pos.Common.Extensions;

public static class FirebaseExtensions
{
    public static IServiceCollection ConfigureFirebase(this IServiceCollection services, IWebHostEnvironment env, ILogger logger)
    {
        try
        {
            GoogleCredential credential;
            if (env.IsProduction())
            {
                credential = GoogleCredential.GetApplicationDefault();
            }
            else
            {
                credential = GoogleCredential.FromFile("private_key.json");
            }

            FirebaseApp.Create(new AppOptions()
            {
                Credential = credential,
                ProjectId = "kayord-pos",
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
        return services;
    }
}