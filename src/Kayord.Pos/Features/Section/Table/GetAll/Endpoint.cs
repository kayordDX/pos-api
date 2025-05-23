using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Section.Table.GetAll
{
    public class Endpoint : Endpoint<Request, List<Pos.Entities.Table>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/section/tables");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var tables = await _dbContext.Table
                .Where(s => s.SectionId == req.SectionId && s.isDeleted != true)
                .OrderBy(x => x.Name)
                .ToListAsync();

            await SendAsync(tables);
        }
    }
}