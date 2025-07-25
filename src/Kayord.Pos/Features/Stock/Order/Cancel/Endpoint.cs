using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Order.Cancel;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/stock/order/cancel");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockOrder.FindAsync(req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var orderItems = await _dbContext.StockOrderItem.Where(x => x.StockOrderId == req.Id).ToListAsync(ct);
        foreach (StockOrderItem item in orderItems)
        {
            item.StockOrderItemStatusId = 3;
        }

        entity.StockOrderStatusId = 3;

        await _dbContext.SaveChangesAsync(ct);
        await Send.NoContentAsync(ct);
    }
}
