using Kayord.Pos.Data;
using Kayord.Pos.Entities;
namespace Kayord.Pos.Features.Stock.Link.Add
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
            Post("/stock/link");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            if (req.LinkType == 0)
            {
                await _dbContext.MenuItemStock.AddAsync(new MenuItemStock
                {
                    MenuItemId = req.Id,
                    StockId = req.StockId,
                    Quantity = req.Quantity,
                }, ct);
            }
            else if (req.LinkType == 1)
            {
                await _dbContext.ExtraStock.AddAsync(new ExtraStock
                {
                    ExtraId = req.Id,
                    StockId = req.StockId,
                    Quantity = req.Quantity,
                }, ct);
            }
            else if (req.LinkType == 2)
            {
                await _dbContext.OptionStock.AddAsync(new OptionStock
                {
                    OptionId = req.Id,
                    StockId = req.StockId,
                    Quantity = req.Quantity,
                }, ct);
            }
            else if (req.LinkType == 3)
            {
                await _dbContext.MenuItemBulkStock.AddAsync(new MenuItemBulkStock
                {
                    MenuItemId = req.Id,
                    StockId = req.StockId,
                    Quantity = req.Quantity,
                }, ct);
            }
            await _dbContext.SaveChangesAsync();
            await SendNoContentAsync();
        }
    }
}



