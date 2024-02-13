using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using YamlDotNet.Core.Tokens;

namespace Kayord.Pos.Features.Order.AddItems
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
            Post("/order/addItems");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            int OrderItemId = 0;
            int TableBookingId = req.TableBookingId;

            foreach (Order order in req.Orders)
            {

                var menuItem = await _dbContext.MenuItem.FindAsync(order.MenuItemId);
                if (menuItem == null)
                {
                    await SendNotFoundAsync();
                    return;
                }
                List<Option> Options = new List<Option>();

                OrderItem entity = new OrderItem()
                {
                    TableBookingId = TableBookingId,
                    MenuItemId = order.MenuItemId

                };

                await _dbContext.OrderItem.AddAsync(entity);


                if (order.OptionIds != null)
                    foreach (var i in order.OptionIds)
                    {
                        OrderItemOption o = new() { OrderItemId = entity.OrderItemId, OptionId = i };
                        await _dbContext.OrderItemOption.AddAsync(o);
                    }

                List<Extra> Extras = new List<Extra>();

                if (order.ExtraIds != null)
                    foreach (int i in order.ExtraIds)
                    {
                        OrderItemExtra e = new() { OrderItemId = entity.OrderItemId, ExtraId = i };
                        await _dbContext.OrderItemExtra.AddAsync(e);
                    }

                OrderItemId = entity.OrderItemId;
            }

            await _dbContext.SaveChangesAsync();
            if (OrderItemId > 0)
                await SendNoContentAsync();
            else
                await SendErrorsAsync(500);
        }
    }
}