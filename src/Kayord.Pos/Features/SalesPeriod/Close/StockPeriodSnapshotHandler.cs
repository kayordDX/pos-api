using Kayord.Pos.Data;
using Kayord.Pos.Events;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.SalesPeriod.Close;

public class StockPeriodSnapshotHandler : IEventHandler<StockPeriodSnapshotEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;

    public StockPeriodSnapshotHandler(IServiceScopeFactory scopeFactory, ILogger<StockPeriodSnapshotHandler> logger)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(StockPeriodSnapshotEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var _dbContext = scope.Resolve<AppDbContext>();

        if (_dbContext == null)
        {
            throw new Exception("Dependency injection failed");
        }

        await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"CALL sp_capture_stock_period_snapshot({eventModel.SalesPeriodId}, {eventModel.CreatedBy}, {eventModel.OutletId})",
            ct
        );
    }
}
