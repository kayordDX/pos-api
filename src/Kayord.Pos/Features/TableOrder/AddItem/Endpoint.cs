using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Order.AddItem
{
    public class Endpoint : Endpoint<Request, Pos.Entities.OrderItem>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/order/{orderId:int}/addItem");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var order = await _dbContext.TableOrder.FindAsync(req.OrderId);
            if (order == null)
            {
                await SendNotFoundAsync();
                return;
            }

            var menuItem = await _dbContext.MenuItem.FindAsync(req.MenuItemId);
            if (menuItem == null)
            {
                await SendNotFoundAsync();
                return;
            }

            Pos.Entities.OrderItem entity = new Pos.Entities.OrderItem()
            {
                OrderId = req.OrderId,
                MenuItemId = req.MenuItemId,
            };

            await _dbContext.OrderItem.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.OrderItem.FindAsync(entity.OrderItemId);
            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(result);
        }
    }
}