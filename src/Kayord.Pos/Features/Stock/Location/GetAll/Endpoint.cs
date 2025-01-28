using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.DTO;
namespace Kayord.Pos.Features.Stock.Location.GetAll
{
    public class Endpoint : Endpoint<Request, List<Entities.StockLocation>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/stock/location");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.StockLocation.Where(x => x.OutletId == req.OutletId).ToListAsync(ct);
            await SendAsync(results);
        }
    }
}



