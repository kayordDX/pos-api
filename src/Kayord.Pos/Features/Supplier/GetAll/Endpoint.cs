using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Supplier.GetAll
{
    public class Endpoint : Endpoint<Request, List<DTO.SupplierDTO>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/supplier");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.Supplier.Where(x => x.OutletId == req.OutletId)
                .ProjectToDto()
                .ToListAsync(ct);

            await SendAsync(results);
        }
    }
}



