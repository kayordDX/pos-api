using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Division.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/division/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        // No Menu Items
        if (await _dbContext.MenuItem.Where(x => x.DivisionId == req.Id).CountAsync() > 0)
        {
            ValidationContext.Instance.ThrowError("Can not delete division with menu items");
        }
        // No Stock Items
        if (await _dbContext.StockItem.Where(x => x.DivisionId == req.Id).CountAsync() > 0)
        {
            ValidationContext.Instance.ThrowError("Can not delete division with stock items");
        }

        var entity = await _dbContext.Division.FirstOrDefaultAsync(x => x.DivisionId == req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        entity.IsDeleted = true;

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}