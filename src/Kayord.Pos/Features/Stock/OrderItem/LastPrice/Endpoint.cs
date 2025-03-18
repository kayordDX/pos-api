using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem.LastPrice;

public class Endpoint : Endpoint<Request, decimal>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/stock/orderItem/lastPrice");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        decimal result = 0;
        var entity = await _dbContext.StockOrderItem
            .AsNoTracking()
            .Where(x => x.StockOrderId < req.StockOrderId && x.StockId == req.StockId)
            .OrderByDescending(x => x.Created)
            .FirstOrDefaultAsync(ct);

        if (entity != null)
        {
            if (entity.OrderAmount == 0)
            {
                await SendAsync(result);
                return;
            }
            result = entity.Price / entity.OrderAmount;
        }
        await SendAsync(result);
    }
}