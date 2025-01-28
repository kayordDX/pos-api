using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Stock.Order.Update;

public class Endpoint : Endpoint<Request, Entities.StockOrder>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/stock/order");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockOrder.FindAsync(req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        entity.OrderNumber = req.OrderNumber;
        entity.StockLocationId = req.StockLocationId;
        entity.SupplierId = req.SupplierId;

        await _dbContext.SaveChangesAsync();
        await SendAsync(entity);
    }
}
