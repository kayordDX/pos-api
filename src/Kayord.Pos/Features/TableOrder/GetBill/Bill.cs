using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.GetBill;

public static class Bill
{
    public static async Task<Response> Get(Request req, AppDbContext _dbContext)
    {

        Response response = new()
        {
            Total = 0
        };
        decimal TotalPayments = 0m;
        var tableBooking = await _dbContext.TableBooking
            .Include(x => x.Adjustments!)
                .ThenInclude(x => x.AdjustmentType)
            .FirstOrDefaultAsync(x => x.Id == req.TableBookingId);
        if (tableBooking?.Adjustments != null)
        {
            response.Adjustments = tableBooking.Adjustments;
        }

        var paymentStatusIds = _dbContext.OrderItemStatus.Where(x => x.isBillable).Select(rd => rd.OrderItemStatusId).ToList();
        if (tableBooking == null)
        {
            throw new Exception("Table not found");
        }
        response.BillDate = tableBooking.CloseDate ?? tableBooking.BookingDate;

        response.OrderItems = await _dbContext.OrderItem
        .Where(x => paymentStatusIds.Contains(x.OrderItemStatusId) && x.TableBookingId == req.TableBookingId)
        .ProjectToDto()
        .ToListAsync();

        response.PaymentsReceived = await _dbContext.Payment.Where(x => x.TableBookingId == req.TableBookingId).ToListAsync();
        response.Total += response.OrderItems.Sum(item => item.MenuItem.Price);

        response.Total += response.OrderItems.Where(item => item.OrderItemOptions != null)
                                      .Sum(item => item.OrderItemOptions!.Sum(option => option.Option.Price));

        response.Total += response.OrderItems.Where(item => item.OrderItemExtras != null)
                                      .Sum(item => item.OrderItemExtras!.Sum(extra => extra.Extra.Price));

        response.Total += response.Adjustments!.Sum(x => x.Amount);

        TotalPayments += response.PaymentsReceived.Where(item => item.TableBookingId! == req.TableBookingId)
                                      .Sum(item => item.Amount);
        response.Balance = response.Total - TotalPayments;
        response.TipAmount = (response.Total - TotalPayments) * -1;
        response.TotalExVAT = Math.Round(response.Total / 1.15m, 2);
        response.VAT = response.Total - response.TotalExVAT;
        response.Balance = response.Balance < 0 ? 0m : response.Balance;

        return response;
    }
}