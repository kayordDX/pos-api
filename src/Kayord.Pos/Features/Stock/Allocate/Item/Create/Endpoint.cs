using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

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
        var stockAllocate = await _dbContext.StockAllocate.Where(x => x.Id == req.StockAllocateId).FirstOrDefaultAsync(ct);
        if (stockAllocate == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var stockItem = await _dbContext.StockItem
            .Where(x => x.StockId == req.StockId && x.DivisionId == stockAllocate.FromDivisionId)
            .FirstOrDefaultAsync(ct);

        if (stockItem == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        if (req.Actual > stockItem.Actual)
        {
            throw new Exception("Not enough stock to allocate");
        }

        var entity = new Entities.StockAllocateItem
        {
            StockAllocateId = req.StockAllocateId,
            StockId = req.StockId,
            Actual = req.Actual,
            StockAllocateItemStatusId = 1,
            AssignedUserId = stockAllocate.AssignedUserId,
        };

        await _dbContext.StockAllocateItem.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
