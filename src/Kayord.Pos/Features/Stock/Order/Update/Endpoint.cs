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
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockOrder.FindAsync(req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        entity.OrderNumber = req.OrderNumber;
        entity.DivisionId = req.DivisionId;
        entity.SupplierId = req.SupplierId;

        await _dbContext.SaveChangesAsync();
        await Send.OkAsync(entity);
    }
}
