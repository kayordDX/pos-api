using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Allocate.Item.Cancel;

public class Endpoint : Endpoint<Request, StockAllocateItem>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _currentUserService;

    public Endpoint(AppDbContext dbContext, CurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public override void Configure()
    {
        Put("/stock/allocate/item/cancel");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockAllocateItem
            .Where(x => x.Id == req.Id)
            .Include(x => x.StockAllocate)
            .FirstOrDefaultAsync(ct);

        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        // Set status to cancelled
        if (entity.StockAllocateItemStatusId == 1 || entity.StockAllocateItemStatusId == 2)
        {
            entity.StockAllocateItemStatusId = 3;
            entity.Completed = DateTime.Now;
        }

        await _dbContext.SaveChangesAsync();
        await AllocateItemUpdate.Status(entity.StockAllocateId, _dbContext, ct);
        await Send.OkAsync(entity);
    }
}
