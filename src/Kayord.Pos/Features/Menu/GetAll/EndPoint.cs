

using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Menu.List
{
    public class Endpoint : Endpoint<Request, List<Entities.Menu>>
    {
        private readonly Data.AppDbContext _dbContext;

        public Endpoint(Data.AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/menu");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var menus = await _dbContext.Menu
                .Where(menu => menu.OutletId == req.OutletId)
                .ToListAsync();

            await SendAsync(menus);
        }
    }
}