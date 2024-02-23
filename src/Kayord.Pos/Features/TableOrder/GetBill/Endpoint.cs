using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.DTO;

using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.GetBill;

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
        Get("/order/getBill");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Response response = new();
        response.Total = 0;
        decimal TotalPayments = 0m;
        var tableBooking = await _dbContext.TableBooking.FirstOrDefaultAsync(x => x.Id == req.TableBookingId);
        var paymentStatusIds = _dbContext.OrderItemStatus.Where(x => x.isCancelled == false).Select(rd => rd.OrderItemStatusId).ToList();
        if (tableBooking == null)
            await SendNotFoundAsync();
        response.OrderItems = await _dbContext.OrderItem
        .Where(x => paymentStatusIds.Contains(x.OrderItemStatusId) && x.TableBookingId == req.TableBookingId)
        .Include(x => x.OrderItemExtras)
        .ProjectToDto()
        .ToListAsync();

        response.PaymentsReceived = await _dbContext.Payment.Where(x => x.TableBookingId == req.TableBookingId).ToListAsync();
        response.Total += response.OrderItems.Sum(item => item.MenuItem.Price);

        response.Total += response.OrderItems.Where(item => item.OrderItemOptions != null)
                                      .Sum(item => item.OrderItemOptions!.Sum(option => option.Option.Price));

        response.Total += response.OrderItems.Where(item => item.OrderItemExtras != null)
                                      .Sum(item => item.OrderItemExtras!.Sum(extra => extra.Extra.Price));

        TotalPayments += response.PaymentsReceived.Where(item => item.TableBookingId! == req.TableBookingId)
                                      .Sum(item => item.Amount);
        response.Balance = response.Total - TotalPayments;
        response.Balance = response.Balance < 0 ? 0m : response.Balance;
        await SendAsync(response);
    }
}