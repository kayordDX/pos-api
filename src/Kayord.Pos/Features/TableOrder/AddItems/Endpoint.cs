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
            int TableBookingId = req.TableBookingId;
            List<OrderItemOption> orderItemOptions = new();
            List<OrderItemExtra> orderItemExtra = new();
            OrderItem entity = new();
            foreach (Order order in req.Orders)
            {

                var menuItem = await _dbContext.MenuItem.FindAsync(order.MenuItemId);
                if (menuItem == null)
                {
                    await SendNotFoundAsync();
                    return;
                }
                List<Option> Options = new List<Option>();

                entity = new OrderItem()
                {
                    TableBookingId = TableBookingId,
                    MenuItemId = order.MenuItemId,
                    OrderItemStatusId = 1,
                    Note = order.Note
                };

                await _dbContext.OrderItem.AddAsync(entity);


                if (order.OptionIds != null)
                    foreach (var i in order.OptionIds)
                    {
                        OrderItemOption o = new() { OrderItemId = entity.OrderItemId, OptionId = i };
                        orderItemOptions.Add(o);
                    }

                List<Extra> Extras = new List<Extra>();

                if (order.ExtraIds != null)
                    foreach (int i in order.ExtraIds)
                    {
                        OrderItemExtra e = new() { OrderItemId = entity.OrderItemId, ExtraId = i };
                        orderItemExtra.Add(e);
                    }
            }

            await _dbContext.SaveChangesAsync();
            foreach (var o in orderItemOptions)
            {
                o.OrderItemId = entity.OrderItemId;
                await _dbContext.OrderItemOption.AddAsync(o);
            }
            foreach (var e in orderItemExtra)
            {
                e.OrderItemId = entity.OrderItemId;
                await _dbContext.OrderItemExtra.AddAsync(e);
            }
            await _dbContext.SaveChangesAsync();

            if (entity.OrderItemId > 0)
                await SendNoContentAsync();
            else
                await SendErrorsAsync(500);
        }
    }
}