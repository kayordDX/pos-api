using Serilog;

namespace Kayord.Pos.Common.Extensions.Host;

public static class LoggingConfiguration
{
    public static void AddLoggingConfiguration(this IHostBuilder host, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        host.UseSerilog();
    }
}