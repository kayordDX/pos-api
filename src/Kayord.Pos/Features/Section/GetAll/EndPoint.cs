using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Section.List
{
    public class Endpoint : Endpoint<Request, List<Pos.Entities.Section>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/section");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var sections = await _dbContext.Section
                .Where(s => s.OutletId == req.OutletId)
                .ToListAsync();

            await SendAsync(sections);
        }
    }
}