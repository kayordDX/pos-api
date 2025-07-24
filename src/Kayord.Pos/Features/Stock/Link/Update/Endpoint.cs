using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Link.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/stock/link");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (req.LinkType == 0)
        {
            var entity = await _dbContext.MenuItemStock.Where(x => x.MenuItemId == req.Id && x.StockId == req.StockId).FirstOrDefaultAsync(ct);
            if (entity == null)
            {
                await SendNotFoundAsync();
                return;
            }
            entity.Quantity = req.Quantity;
        }
        else if (req.LinkType == 1)
        {
            var entity = await _dbContext.ExtraStock.Where(x => x.ExtraId == req.Id && x.StockId == req.StockId).FirstOrDefaultAsync(ct);
            if (entity == null)
            {
                await SendNotFoundAsync();
                return;
            }
            entity.Quantity = req.Quantity;
        }
        else if (req.LinkType == 2)
        {
            var entity = await _dbContext.OptionStock.Where(x => x.OptionId == req.Id && x.StockId == req.StockId).FirstOrDefaultAsync(ct);
            if (entity == null)
            {
                await SendNotFoundAsync();
                return;
            }
            entity.Quantity = req.Quantity;
        }
        else if (req.LinkType == 3)
        {
            var entity = await _dbContext.MenuItemBulkStock.Where(x => x.MenuItemId == req.Id && x.StockId == req.StockId).FirstOrDefaultAsync(ct);
            if (entity == null)
            {
                await SendNotFoundAsync();
                return;
            }
            entity.Quantity = req.Quantity;
        }
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}



