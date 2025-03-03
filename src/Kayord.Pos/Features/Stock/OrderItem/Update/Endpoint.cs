using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem.Update;

public class Endpoint : Endpoint<Request, Entities.StockOrderItem>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/stock/orderItem");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockOrderItem.Where(x => x.StockOrderId == req.StockOrderId && x.StockId == req.StockId).FirstOrDefaultAsync(ct);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        entity.OrderAmount = req.OrderAmount;
        entity.Actual = req.Actual;
        entity.Price = req.Price;
        entity.StockOrderItemStatusId = req.StockOrderItemStatusId;

        await _dbContext.SaveChangesAsync();
        await SendAsync(entity);
    }
}
