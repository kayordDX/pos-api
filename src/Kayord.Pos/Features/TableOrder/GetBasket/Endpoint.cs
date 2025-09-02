using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.GetBasket;

public class Endpoint : Endpoint<Request, Response>
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
        Get("/order/getBasket");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Response response = new()
        {
            Total = 0
        };
        var tableBooking = await _dbContext.TableBooking.FirstOrDefaultAsync(x => x.Id == req.TableBookingId);
        if (tableBooking == null)
            await Send.NotFoundAsync();
        response.OrderItems = await _dbContext.OrderItem
            .Where(x => x.TableBookingId == req.TableBookingId && x.OrderItemStatusId == 1)
            .ProjectToDto()
            .ToListAsync();

        foreach (BillOrderItemDTO item in response.OrderItems)
        {
            response.Total += item.MenuItem.Price;
            if (item.OrderItemOptions != null)
            {
                foreach (OrderItemOptionDTO option in item.OrderItemOptions)
                {
                    response.Total += option.Option.Price;
                    item.MenuItem.Price += option.Option.Price;
                }
            }
            if (item.OrderItemExtras != null)
            {
                foreach (OrderItemExtraDTO extra in item.OrderItemExtras)
                {
                    response.Total += extra.Extra.Price;
                    item.MenuItem.Price += extra.Extra.Price;
                }
            }
            item.Quantity = response.OrderItems.Sum(x => x.MenuItemId == item.MenuItemId ? 1 : 0);
        }
        await Send.OkAsync(response);
    }
}