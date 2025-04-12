using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Stock.Allocate.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Put("/stock/allocate");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockAllocate
            .Where(x => x.Id == req.Id)
            .FirstOrDefaultAsync(ct);

        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        entity.StockAllocateStatusId = req.StockAllocateStatusId;

        // If status is in progress make all child items waiting
        if (entity.StockAllocateStatusId == 2)
        {
            var items = await _dbContext.StockAllocateItem.Where(x => x.StockAllocateId == entity.Id).ToListAsync(ct);
            foreach (var item in items)
            {
                item.StockAllocateItemStatusId = 2;
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}
