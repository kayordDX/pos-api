using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill.EmailBill;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Bill;

public static class BillHelper
{
    public static async Task<Response> Get(int tableBookingId, AppDbContext _dbContext)
    {

        Response response = new()
        {
            Total = 0
        };
        decimal TotalPayments = 0m;
        var tableBooking = await _dbContext.TableBooking
            .Include(x => x.Adjustments!)
                .ThenInclude(x => x.AdjustmentType)
            .FirstOrDefaultAsync(x => x.Id == tableBookingId);

        response.IsCashedUp = tableBooking?.CashUpUserId != null;

        if (tableBooking?.Adjustments != null)
        {
            response.Adjustments = tableBooking.Adjustments;
        }

        var paymentStatusIds = _dbContext.OrderItemStatus.Where(x => x.IsBillable).Select(rd => rd.OrderItemStatusId).ToList();
        if (tableBooking == null)
        {
            throw new Exception("Table not found");
        }
        response.BillDate = tableBooking.CloseDate ?? tableBooking.BookingDate;

        response.OrderItems = await _dbContext.OrderItem
        .Where(x => paymentStatusIds.Contains(x.OrderItemStatusId) && x.TableBookingId == tableBookingId)
        .ProjectToDto()
        .ToListAsync();

        response.PaymentsReceived = await _dbContext.Payment
            .Where(x => x.TableBookingId == tableBookingId)
            .Include(x => x.PaymentType)
            .ToListAsync();

        response.Total += response.OrderItems.Sum(item => item.MenuItem.Price);

        response.Total += response.OrderItems.Where(item => item.OrderItemOptions != null)
            .Sum(item => item.OrderItemOptions!.Sum(option => option.Option.Price));

        response.Total += response.OrderItems.Where(item => item.OrderItemExtras != null)
            .Sum(item => item.OrderItemExtras!.Sum(extra => extra.Extra.Price));

        response.Total += response.Adjustments!.Sum(x => x.Amount);

        TotalPayments += response.PaymentsReceived.Where(item => item.TableBookingId! == tableBookingId)
            .Sum(item => item.Amount);

        response.Total = response.Total < 0 ? 0m : response.Total;
        response.Balance = response.Total - TotalPayments;
        response.TipAmount = (response.Total - TotalPayments) * -1;
        var vatRateEntity = await _dbContext.VATRate
            .AsNoTracking()
            .Where(x => tableBooking.BookingDate >= x.StartDate && tableBooking.BookingDate <= x.EndDate)
            .FirstOrDefaultAsync();
        if (vatRateEntity == null)
        {
            throw new Exception("Vat rate not found");
        }
        decimal vatRate = 1 + vatRateEntity.Value;

        response.TotalExVAT = Math.Round(response.Total / vatRate, 2);
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
            .Include(x => x.Adjustments!)
                .ThenInclude(x => x.AdjustmentType)
            .FirstOrDefaultAsync(x => x.Id == tableBookingId);

        var paymentStatusIds = _dbContext.OrderItemStatus.Where(x => x.IsBillable).Select(rd => rd.OrderItemStatusId).ToList();
        if (tableBooking == null)
        {
            throw new Exception("Table not found");
        }

        var orderItems = await _dbContext.OrderItem
            .Where(x => paymentStatusIds.Contains(x.OrderItemStatusId) && x.TableBookingId == tableBookingId)
            .ProjectToDto()
            .ToListAsync();

        var payments = await _dbContext.Payment
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

    public static async Task<PdfRequest> GetPdfRequestAsync(int tableBookingId, AppDbContext _dbContext)
    {
        try
        {
            TableOrder.GetBill.Request request = new() { TableBookingId = tableBookingId };
            var bill = await TableOrder.GetBill.Bill.Get(request, _dbContext);

            List<Item> items = new();
            foreach (var order in bill.SummaryOrderItems)
            {
                List<SubItem> subItems = new();

                foreach (var extra in order.OrderItemExtras ?? [])
                {
                    subItems.Add(new SubItem { Name = $"+ {extra.Extra.Name}", Price = extra.Extra.Price, TotalPrice = order.ExtrasTotal });
                }
                foreach (var option in order.OrderItemOptions ?? [])
                {
                    subItems.Add(new SubItem { Name = $"> {option.Option.Name}", Price = option.Option.Price, TotalPrice = order.OptionsTotal });
                }
                items.Add(new Item { Name = order.MenuItem.Name, Price = order.MenuItem.Price, Items = subItems, Count = order.Quantity, TotalPrice = order.Total });
            }
            foreach (var adjustment in bill.Adjustments ?? [])
            {
                items.Add(new Item { Name = adjustment.AdjustmentType.Name, Price = adjustment.Amount });
            }

            var tableBooking = await _dbContext.TableBooking.FindAsync(tableBookingId);
            if (tableBooking == null)
            {
                throw new Exception("No booking found");
            }

            var salesPeriod = await _dbContext.SalesPeriod.FindAsync(tableBooking.SalesPeriodId);
            if (salesPeriod == null)
            {
                throw new Exception("No sales period found");
            }

            var outlet = await _dbContext.Outlet.Include(x => x.Business).FirstOrDefaultAsync(x => x.Id == salesPeriod.OutletId);
            if (outlet == null)
            {
                throw new Exception("No outlet found");
            }

            // Generate PDF
            PdfRequest pdfRequest = new()
            {
                Balance = bill.Balance,
                VAT = bill.VAT,
                PaymentReceived = bill.PaymentsReceived.Sum(s => s.Amount),
                TableBookingId = tableBookingId,
                TipAmount = bill.TipAmount,
                TotalExVAT = bill.TotalExVAT,
                Total = bill.Total,
                BillDate = bill.BillDate,
                Items = items,
                OutletName = $"{outlet.DisplayName}",
                VATNumber = outlet.VATNumber,
                Logo = outlet.Logo,
                Address = outlet.Address,
                Company = outlet.Company,
                Registration = outlet.Registration,
                IsClosed = bill.IsClosed,
                TableName = bill.TableName,
                Waiter = bill.Waiter,
                Divisions = bill.Divisions,
            };
            return pdfRequest;
        }
        catch (Exception)
        {
            throw;
        }
    }
}