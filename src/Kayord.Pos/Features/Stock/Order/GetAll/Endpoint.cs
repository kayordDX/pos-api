using Kayord.Pos.Data;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Order.GetAll
{
    public class Endpoint : Endpoint<Request, PaginatedList<Entities.StockOrder>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/stock/order");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.StockOrder
                .Include(x => x.Division)
                .Include(x => x.Supplier)
                .Include(x => x.StockOrderStatus)
                .GetPagedAsync(req, ct);
            await SendAsync(results);
        }
    }
}



