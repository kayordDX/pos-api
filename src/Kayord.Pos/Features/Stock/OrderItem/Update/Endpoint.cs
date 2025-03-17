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

        var request = new OrderItem.Request()
        {
            Actual = req.Actual,
            StockId = req.StockId,
            StockOrderId = req.StockOrderId,
            StockOrderItemStatusId = req.StockOrderItemStatusId
        };

        await OrderItemUpdate.UpdateOrderItem(entity, request, _dbContext, _currentUserService, ct);

        entity.OrderAmount = req.OrderAmount;
        entity.Actual = req.Actual;
        entity.Price = req.Price;
        entity.StockOrderItemStatusId = req.StockOrderItemStatusId;

        await _dbContext.SaveChangesAsync();
        await SendAsync(entity);
    }
}
