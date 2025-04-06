using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.DTO;
namespace Kayord.Pos.Features.Stock.Allocate.GetAll
{
    public class Endpoint : Endpoint<Request, PaginatedList<StockAllocateDTO>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/stock/allocate");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.StockAllocate
                .Where(x => x.OutletId == req.OutletId)
                .ProjectToDto()
                .GetPagedAsync(req, ct);
            await SendAsync(results);
        }
    }
}



