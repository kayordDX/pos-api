using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Items.UpdateStockTake;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/stock/items/stockTake");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockItem.FirstOrDefaultAsync(x => x.Id == req.StockItemId, ct);

        if (entity == null)
        {
            ValidationContext.Instance.ThrowError("Could not find stock item");
        }

        entity.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
