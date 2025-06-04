using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Link.Delete
{
    public class Endpoint : Endpoint<Request>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Delete("/stock/link/{id}/{stockId}/{linkType}");
            Policies(Constants.Policy.Manager);
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            if (req.LinkType == 0)
            {
                await _dbContext.MenuItemStock.Where(x => x.MenuItemId == req.Id && x.StockId == req.StockId).ExecuteDeleteAsync(ct);
            }
            else if (req.LinkType == 1)
            {
                await _dbContext.ExtraStock.Where(x => x.ExtraId == req.Id && x.StockId == req.StockId).ExecuteDeleteAsync(ct);
            }
            else if (req.LinkType == 2)
            {
                await _dbContext.OptionStock.Where(x => x.OptionId == req.Id && x.StockId == req.StockId).ExecuteDeleteAsync(ct);
            }
            else if (req.LinkType == 3)
            {
                await _dbContext.MenuItemBulkStock.Where(x => x.MenuItemId == req.Id && x.StockId == req.StockId).ExecuteDeleteAsync(ct);
            }
            await SendNoContentAsync();
        }
    }
}



