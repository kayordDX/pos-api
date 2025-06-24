using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.StockCategory.GetAll
{
    public class Endpoint : Endpoint<Request, List<Entities.StockCategory>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/stockCategory");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            if (req.parentId == 0)
            {
                if (req.parentOnly)
                    await SendAsync(await _dbContext.StockCategory.Where(x => x.IsDeleted == false && x.OutletId == req.OutletId && (x.ParentId == null || x.ParentId == 0)).ToListAsync());
                else
                    await SendAsync(await _dbContext.StockCategory.Where(x => x.IsDeleted == false && x.OutletId == req.OutletId && x.ParentId != null).ToListAsync());
            }
            else
            {
                await SendAsync(await _dbContext.StockCategory.Where(x => x.IsDeleted == false && x.OutletId == req.OutletId && x.ParentId == req.parentId).ToListAsync());
            }
        }
    }
}



