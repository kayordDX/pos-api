using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Common.Extensions;

public class MigratorHostedService : IHostedService
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<MigratorHostedService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public MigratorHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<MigratorHostedService> logger, IWebHostEnvironment env)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _env = env;
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

            // Development Seed
            if (_env.IsDevelopment() || _env.IsEnvironment("Testing"))
            {
                await AppDbSeed.SeedData(context, cancellationToken);
            }

            // Production Seed
            await ProdSeed.SeedData(context, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
