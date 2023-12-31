

using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Menu.List
{
    public class Endpoint : Endpoint<Request, List<Pos.Entities.Menu>>
    {
        private readonly Data.AppDbContext _dbContext;

        public Endpoint(Data.AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/menu");
            AllowAnonymous();
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