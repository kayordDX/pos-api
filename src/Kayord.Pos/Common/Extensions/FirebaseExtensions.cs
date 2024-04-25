using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Serilog;
namespace Kayord.Pos.Common.Extensions;

public static class FirebaseExtensions
{
    public static IServiceCollection ConfigureFirebase(this IServiceCollection services, IWebHostEnvironment env)
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
            Log.Logger.Error(ex.Message);
        }
        return services;
    }
}