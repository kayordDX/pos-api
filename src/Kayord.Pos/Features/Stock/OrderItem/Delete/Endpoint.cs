using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.OrderItem.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/stock/orderItem/{stockId}/{stockOrderId}");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockOrderItem.FirstOrDefaultAsync(x => x.StockId == req.StockId && x.StockOrderId == req.StockOrderId);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }
        _dbContext.StockOrderItem.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}