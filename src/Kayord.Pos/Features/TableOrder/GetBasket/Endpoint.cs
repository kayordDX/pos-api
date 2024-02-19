using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Kayord.Pos.DTO;

using Microsoft.EntityFrameworkCore;
using SqlKata;
using Kayord.Pos.Common.Wrapper;

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
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Response response = new();
        response.Total = 0;
        var tableBooking = await _dbContext.TableBooking.FirstOrDefaultAsync(x => x.Id == req.TableBookingId);
        if (tableBooking == null)
            await SendNotFoundAsync();
        response.OrderItems = await _dbContext.OrderItem
        .Where(x => x.TableBookingId == req.TableBookingId && x.OrderItemStatusId == 1)
        .ProjectToDto()
        .ToListAsync();

        foreach (BillOrderItemDTO item in response.OrderItems)
        {
            response.Total += item.MenuItem.Price;
            if (item.Options != null)
                foreach (OptionDTO option in item.Options)
                {
                    response.Total += option.Price;
                }
            if (item.Extras != null)
                foreach (ExtraDTO extra in item.Extras)
                {
                    response.Total += extra.Price;
                }
        }
        await SendAsync(response);
    }
}