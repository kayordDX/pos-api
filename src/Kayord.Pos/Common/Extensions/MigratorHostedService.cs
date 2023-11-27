using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Common.Extensions;

public class MigratorHostedService : IHostedService
{
    private readonly ILogger<MigratorHostedService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public MigratorHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<MigratorHostedService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceScopeFactory.CreateScope();
            AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (context.Database.IsNpgsql())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }
            // Seed
            await AppDbSeed.SeedData(context, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
