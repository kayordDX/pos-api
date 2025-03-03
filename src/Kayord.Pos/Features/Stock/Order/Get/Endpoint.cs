using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Kayord.Pos.DTO;
namespace Kayord.Pos.Features.Stock.Order.Get
{
    public class Endpoint : Endpoint<Request, StockOrderDTO>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/stock/order/{Id}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.StockOrder
                .Where(x => x.Id == req.Id)
                .ProjectToDto()
                .FirstOrDefaultAsync(ct);

            if (results == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(results);
        }
    }
}



