using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem.LastPrice;

public class Endpoint : Endpoint<Request, Response>
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
        decimal amount = await _dbContext.StockItem
            .Where(x => x.StockId == req.StockId)
            .GroupBy(x => x.Actual)
            .Select(x => x.Sum(t => t.Actual))
            .FirstOrDefaultAsync(ct);

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
                result = 0;
            }
            else
            {
                result = entity.Price / entity.OrderAmount;
            }
        }

        Response response = new() { LastPrice = result, TotalAmount = amount };
        await SendAsync(response);
    }
}