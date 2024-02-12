using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Features.Table.GetAvailable;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Menu.GetItem
{
    public class GetMenuItemsEndpoint : Endpoint<Request, MenuItemDTO>
    {
        private readonly AppDbContext _dbContext;

        public GetMenuItemsEndpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/menu/item");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var result = await _dbContext.MenuItem
                .Include(m => m.MenuItemOptionGroups)
                    .ThenInclude(m => m.OptionGroup)
                .Where(x => x.MenuItemId.Equals(req.Id))
                .ProjectToDto()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }
            await SendAsync(result);
        }
    }
}
