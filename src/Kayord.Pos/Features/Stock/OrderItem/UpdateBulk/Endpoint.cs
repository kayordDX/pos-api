using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem.UpdateBulk;

public class Endpoint : Endpoint<Request>
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
        Put("/stock/orderItem/bulk");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (req.StockIds == null)
        {
            await SendNotFoundAsync();
            return;
        }

        List<int> stockCheck = [];

        foreach (int id in req.StockIds)
        {
            var entity = await _dbContext.StockOrderItem
                .Where(x => x.StockOrderId == req.StockOrderId && x.StockId == id)
                .Include(x => x.StockOrder)
                .FirstOrDefaultAsync(ct);

            if (entity == null)
            {
                await SendNotFoundAsync();
                return;
            }

            decimal actual = entity.Actual;
            if (req.StockOrderItemStatusId == 2)
            {
                entity.Actual = entity.OrderAmount;
            }
            await OrderItemUpdate.StockCount(req.StockOrderId, actual, entity.StockOrder.DivisionId, entity.StockId, entity.Actual, _dbContext, _currentUserService, ct);
            entity.StockOrderItemStatusId = req.StockOrderItemStatusId;

            if (!stockCheck.Contains(entity.StockId))
            {
                stockCheck.Add(entity.StockId);
            }
        }

        await _dbContext.SaveChangesAsync();

        foreach (int s in stockCheck)
        {
            await StockManager.StockAvailableCheck(s, _dbContext, ct);
        }

        await OrderItemUpdate.StockOrderStatus(req.StockOrderId, _dbContext, ct);
        await SendNoContentAsync();
    }
}
