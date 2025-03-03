using Kayord.Pos.Data;
using Kayord.Pos.Services;


namespace Kayord.Pos.Features.Stock.Order.Create;

public class Endpoint : Endpoint<Request, Entities.StockOrder>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/stock/order");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new Entities.StockOrder
        {
            OutletId = req.OutletId,
            OrderNumber = req.OrderNumber,
            DivisionId = req.DivisionId,
            SupplierId = req.SupplierId,
            StockOrderStatusId = 1,
        };

        await _dbContext.StockOrder.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
