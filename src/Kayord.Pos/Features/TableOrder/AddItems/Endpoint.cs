using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Order.AddItems;

public class Endpoint : Endpoint<Request, OrderItem>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Post("/order/addItems");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var tableBooking = await _dbContext.TableBooking
            .Include(x => x.SalesPeriod)
            .FirstOrDefaultAsync(x => x.Id == req.TableBookingId);

        if (tableBooking == null)
        {
            throw new Exception("No booking found");
        }
        else
        {
            if (tableBooking.CloseDate != null)
            {
                throw new Exception("Table is closed");
            }
        }

        int tableBookingOutletId = tableBooking.SalesPeriod.OutletId;

        OrderItem orderItem = new();
        foreach (Order order in req.Orders)
        {
            for (int q = 1; q <= order.Quantity; q++)
            {
                var menuItem = await _dbContext.MenuItem
                    .Include(x => x.MenuSection)!
                        .ThenInclude(x => x.Menu)
                    .FirstOrDefaultAsync(x => x.MenuItemId == order.MenuItemId, ct);

                if (menuItem == null)
                {
                    await Send.NotFoundAsync();
                    return;
                }

                int menuOutletId = menuItem.MenuSection.Menu.OutletId;

                if (tableBookingOutletId != menuOutletId)
                {
                    throw new Exception("Outlet mismatch");
                }

                List<Entities.Option> Options = new List<Entities.Option>();

                orderItem = new OrderItem()
                {
                    TableBookingId = req.TableBookingId,
                    MenuItemId = order.MenuItemId,
                    OrderItemStatusId = 1,
                    Note = order.Note,
                    UserId = _cu.UserId
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
            await Send.NoContentAsync();
        else
            await Send.ErrorsAsync(500);
    }
}