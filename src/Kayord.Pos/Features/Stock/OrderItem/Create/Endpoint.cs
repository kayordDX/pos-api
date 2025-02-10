using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Stock.OrderItem.Create;

public class Endpoint : Endpoint<Request, Entities.StockOrder>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/stock/orderItem");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new Entities.StockOrderItem
        {
            StockOrderId = req.StockOrderId,
            StockId = req.StockId,
            OrderAmount = req.OrderAmount,
            StockOrderItemStatusId = 1,
            Price = req.Price,
        };

        await _dbContext.StockOrderItem.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
