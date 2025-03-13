using Kayord.Pos.Data;
using Kayord.Pos.Events;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Stock;

public class StockHandler : IEventHandler<StockEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly CurrentUserService _currentUserService;

    public StockHandler(IServiceScopeFactory scopeFactory, CurrentUserService currentUserService)
    {
        _scopeFactory = scopeFactory;
        _currentUserService = currentUserService;
    }

    public async Task HandleAsync(StockEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var _dbContext = scope.Resolve<AppDbContext>();

        if (_dbContext == null)
        {
            throw new Exception("Dependency injection failed");
        }

        await StockManager.StockUpdate(eventModel.OrderItemIds, _dbContext, _currentUserService.UserId ?? "", ct);
    }
}