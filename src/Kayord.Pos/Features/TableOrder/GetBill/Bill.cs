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
            .AsNoTracking()
            .Include(x => x.Adjustments!)
                .ThenInclude(x => x.AdjustmentType)
            .Include(x => x.Table)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == req.TableBookingId);

        response.IsCashedUp = tableBooking?.CashUpUserId != null;

        if (tableBooking?.Adjustments != null)
        {
            response.Adjustments = tableBooking.Adjustments;
        }

        var paymentStatusIds = _dbContext.OrderItemStatus.AsNoTracking().Where(x => x.isBillable).Select(rd => rd.OrderItemStatusId).ToList();
        if (tableBooking == null)
        {
            throw new Exception("Table not found");
        }

        response.TableName = tableBooking.Table.Name;
        response.Waiter = tableBooking.User.Name;
        response.IsClosed = tableBooking.CloseDate != null;

        response.BillDate = tableBooking.CloseDate ?? tableBooking.BookingDate;

        response.OrderItems = await _dbContext.OrderItem
        .AsNoTracking()
        .Where(x => paymentStatusIds.Contains(x.OrderItemStatusId) && x.TableBookingId == req.TableBookingId)
        .ProjectToDto()
        .ToListAsync();

        response.PaymentsReceived = await _dbContext.Payment
            .AsNoTracking()
            .Where(x => x.TableBookingId == req.TableBookingId)
            .Include(x => x.PaymentType)
            .ToListAsync();

        response.Total += response.OrderItems.Sum(item => item.MenuItem.Price);

        response.Total += response.OrderItems.Where(item => item.OrderItemOptions != null)
            .Sum(item => item.OrderItemOptions!.Sum(option => option.Option.Price));

        response.Total += response.OrderItems.Where(item => item.OrderItemExtras != null)
            .Sum(item => item.OrderItemExtras!.Sum(extra => extra.Extra.Price));

        response.Total += response.Adjustments!.Sum(x => x.Amount);

        TotalPayments += response.PaymentsReceived.Where(item => item.TableBookingId! == req.TableBookingId)
            .Sum(item => item.Amount);

        response.Total = response.Total < 0 ? 0m : response.Total;
        response.Balance = response.Total - TotalPayments;
        response.TipAmount = (response.Total - TotalPayments) * -1;
        response.TotalExVAT = Math.Round(response.Total / 1.15m, 2);
        response.VAT = response.Total - response.TotalExVAT;
        response.Balance = response.Balance < 0 ? 0m : response.Balance;
        response.VAT = response.VAT < 0 ? 0m : response.VAT;
        response.TipAmount = response.TipAmount < 0 ? 0m : response.TipAmount;
        response.TotalExVAT = response.TotalExVAT < 0 ? 0m : response.TotalExVAT;

        return response;
    }

    public static async Task<TableTotal> GetTotal(int tableBookingId, AppDbContext _dbContext)
    {
        decimal total = 0;
        decimal totalPayments = 0;

        var tableBooking = await _dbContext.TableBooking
            .AsNoTracking()
            .Include(x => x.Adjustments!)
                .ThenInclude(x => x.AdjustmentType)
            .FirstOrDefaultAsync(x => x.Id == tableBookingId);

        var paymentStatusIds = _dbContext.OrderItemStatus.Where(x => x.isBillable).Select(rd => rd.OrderItemStatusId).ToList();
        if (tableBooking == null)
        {
            throw new Exception("Table not found");
        }

        var orderItems = await _dbContext.OrderItem
            .AsNoTracking()
            .Where(x => paymentStatusIds.Contains(x.OrderItemStatusId) && x.TableBookingId == tableBookingId)
            .ProjectToDto()
            .ToListAsync();

        var payments = await _dbContext.Payment
            .AsNoTracking()
            .Where(x => x.TableBookingId == tableBookingId)
            .Include(x => x.PaymentType)
            .ToListAsync();

        total += orderItems.Sum(item => item.MenuItem.Price);

        total += orderItems.Where(item => item.OrderItemOptions != null)
            .Sum(item => item.OrderItemOptions!.Sum(option => option.Option.Price));

        total += orderItems.Where(item => item.OrderItemExtras != null)
            .Sum(item => item.OrderItemExtras!.Sum(extra => extra.Extra.Price));

        total += tableBooking.Adjustments?.Sum(x => x.Amount) ?? 0;

        totalPayments += payments.Where(item => item.TableBookingId! == tableBookingId)
            .Sum(item => item.Amount);

        total = total < 0 ? 0 : total;
        TableTotal tableTotal = new()
        {
            Total = total,
            TotalPayments = totalPayments,
            TipTotal = totalPayments - total
        };

        return tableTotal;
    }
}