using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kayord.Pos.Features.Stock.OrderItem.UpdateBulk;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
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

        foreach (int id in req.StockIds)
        {
            var entity = await _dbContext.StockOrderItem
            .Where(x => x.StockOrderId == req.StockOrderId && x.StockId == id)
            .FirstOrDefaultAsync(ct);

            if (entity == null)
            {
                await SendNotFoundAsync();
                return;
            }
            entity.Actual = entity.OrderAmount;
            entity.StockOrderItemStatusId = req.StockOrderItemStatusId;
        }

        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}
