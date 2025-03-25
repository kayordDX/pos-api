using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem.Update;

public class Endpoint : Endpoint<Request, StockOrderItem>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _currentUserService;

    public Endpoint(AppDbContext dbContext, CurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public override void Configure()
    {
        Put("/stock/orderItem");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockOrderItem
            .Where(x => x.StockOrderId == req.StockOrderId && x.StockId == req.StockId)
            .Include(x => x.StockOrder)
            .FirstOrDefaultAsync(ct);

        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await OrderItemUpdate.StockCount(req.StockOrderId, entity.Actual, entity.StockOrder.DivisionId, entity.StockId, req.Actual, _dbContext, _currentUserService, ct);

        entity.OrderAmount = req.OrderAmount;
        entity.Actual = req.Actual;
        entity.Price = req.Price;
        entity.StockOrderItemStatusId = req.StockOrderItemStatusId;

        await _dbContext.SaveChangesAsync();

        await OrderItemUpdate.StockOrderStatus(req.StockOrderId, _dbContext, ct);
        await StockManager.StockAvailableCheck(entity.StockId, _dbContext, ct);
        await SendAsync(entity);
    }
}
