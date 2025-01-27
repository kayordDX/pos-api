using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.DTO;
namespace Kayord.Pos.Features.Stock.GetAll
{
    public class GetMenuItemsEndpoint : Endpoint<Request, PaginatedList<StockDTO>>
    {
        private readonly AppDbContext _dbContext;

        public GetMenuItemsEndpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/stock");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.Stock
                .ProjectToDto()
                .GetPagedAsync(req, ct);

            foreach (var item in results.Items)
            {
                item.TotalActual = item?.StockItems?.Sum(x => x.Actual) ?? 0;
            }

            await SendAsync(results);
        }
    }
}



