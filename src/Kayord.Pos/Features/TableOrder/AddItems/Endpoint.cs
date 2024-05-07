using Kayord.Pos.Data;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.Order.AddItems;

public class Endpoint : Endpoint<Request, OrderItem>
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
        OrderItem orderItem = new();
        foreach (Order order in req.Orders)
        {
            for (int q = 1; q <= order.Quantity; q++)
            {
                var menuItem = await _dbContext.MenuItem.FindAsync(order.MenuItemId);
                if (menuItem == null)
                {
                    await SendNotFoundAsync();
                    return;
                }
                List<Option> Options = new List<Option>();

                orderItem = new OrderItem()
                {
                    TableBookingId = TableBookingId,
                    MenuItemId = order.MenuItemId,
                    OrderItemStatusId = 1,
                    Note = order.Note
                };

                if (order.OptionIds != null)
                {
                    List<OrderItemOption> orderItemOptions = new();

                    foreach (var i in order.OptionIds)
                    {
                        OrderItemOption o = new() { OrderItemId = orderItem.OrderItemId, OptionId = i };
                        orderItemOptions.Add(o);
                    }
                    orderItem.OrderItemOptions = orderItemOptions;
                }
                if (order.ExtraIds != null)
                {
                    List<OrderItemExtra> orderItemExtra = new();
                    foreach (int i in order.ExtraIds)
                    {
                        OrderItemExtra e = new() { OrderItemId = orderItem.OrderItemId, ExtraId = i };
                        orderItemExtra.Add(e);
                    }
                    orderItem.OrderItemExtras = orderItemExtra;
                }

                await _dbContext.OrderItem.AddAsync(orderItem);
            }
        }

        await _dbContext.SaveChangesAsync();

        if (orderItem.OrderItemId > 0)
            await SendNoContentAsync();
        else
            await SendErrorsAsync(500);
    }
}