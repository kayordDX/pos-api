using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Menu.CreateMenuItem
{
    public class Endpoint : Endpoint<Request, Pos.Entities.MenuItem>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/menu/{menuId:int}/menuitem");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var menu = await _dbContext.Menu.FindAsync(req.MenuId);
            if (menu == null)
            {
                await SendNotFoundAsync();
                return;
            }

            Pos.Entities.MenuItem entity = new Pos.Entities.MenuItem()
            {
                MenuId = req.MenuId,
                Name = req.Name,
                Price = req.Price,
                // Set other properties based on the request
            };

            await _dbContext.MenuItem.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.MenuItem.FindAsync(entity.MenuItemId);
            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(result);
        }
    }
}