using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Printer.List
{
    public class Endpoint : Endpoint<Request, List<PrinterDTO>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/printer/{outletId}");
        }

        public override async Task HandleAsync(Request r, CancellationToken ct)
        {
            var result = await _dbContext.Printer.Where(x => x.OutletId == r.OutletId).ProjectToDto().ToListAsync();
            await SendAsync(result);
        }
    }
}