using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Stock.Allocate.Item.Create;

public class Endpoint : Endpoint<Request, Entities.StockOrder>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/stock/allocate/item");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new Entities.StockAllocateItem
        {
            StockAllocateId = req.StockAllocateId,
            StockId = req.StockId,
            Actual = req.Actual,
            StockAllocateItemStatusId = 1,
        };

        await _dbContext.StockAllocateItem.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
