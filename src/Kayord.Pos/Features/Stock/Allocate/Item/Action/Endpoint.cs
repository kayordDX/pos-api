using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Allocate.Item.Action;

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
        Put("/stock/allocate/item/action");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockAllocateItem
            .Where(x => x.Id == req.Id)
            .Include(x => x.StockAllocate)
            .FirstOrDefaultAsync(ct);

        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        // Set status to cancelled
        if (entity.StockAllocateItemStatusId == 1 || entity.StockAllocateItemStatusId == 2)
        {
            bool canAllocateStock = true;

            // If Approved update counts and do audit
            if (req.StockAllocateItemStatusId == 4)
            {
                canAllocateStock = await AllocateItemUpdate.StockCount(entity, _dbContext, _currentUserService, ct);
            }

            if (!canAllocateStock)
            {
                throw new Exception("Not enough stock to allocate");
            }
            entity.StockAllocateItemStatusId = req.StockAllocateItemStatusId;
        }

        await _dbContext.SaveChangesAsync();

        await AllocateItemUpdate.Status(entity.StockAllocateId, _dbContext, ct);
        await SendAsync(entity);
    }
}
