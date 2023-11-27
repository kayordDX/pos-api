using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Menu.ListMenuItems
{
    public class Endpoint : Endpoint<Request, List<Pos.Entities.MenuItem>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/menu/{menuId:int}/menuitem");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var menuItems = await _dbContext.MenuItem
                .Where(item => item.MenuId == req.MenuId)
                .ToListAsync();

            await SendAsync(menuItems);
        }
    }
}