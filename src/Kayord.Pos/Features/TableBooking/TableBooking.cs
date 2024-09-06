using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking;

public static class TableBooking
{
    public static async Task SaveTotal(int tableBookingId, AppDbContext _dbContext)
    {
        var booking = await _dbContext.TableBooking.Where(x => x.Id == tableBookingId).FirstOrDefaultAsync();
        if (booking == null)
        {
            throw new Exception("No booking found");
        }
        var billTotal = await BillHelper.GetTotal(tableBookingId, _dbContext);
        booking.Total = billTotal.Total;
        booking.TotalPayments = billTotal.TotalPayments;
        booking.TotalTips = billTotal.TipTotal;
        await _dbContext.SaveChangesAsync();
    }
}