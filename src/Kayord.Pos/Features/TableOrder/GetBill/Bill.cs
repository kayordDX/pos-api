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

        var paymentStatusIds = _dbContext.OrderItemStatus.AsNoTracking().Where(x => x.IsBillable).Select(rd => rd.OrderItemStatusId).ToList();
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
        decimal menuItemPrice = response.OrderItems.Sum(item => item.MenuItem.Price);
        response.Total += menuItemPrice;

        decimal optionPrice = response.OrderItems.Where(item => item.OrderItemOptions != null)
            .Sum(item => item.OrderItemOptions!.Sum(option => option.Option.Price));
        response.Total += optionPrice;

        decimal extrasPrice = response.OrderItems.Where(item => item.OrderItemExtras != null)
            .Sum(item => item.OrderItemExtras!.Sum(extra => extra.Extra.Price));
        response.Total += extrasPrice;

        response.Total += response.Adjustments!.Sum(x => x.Amount);

        TotalPayments += response.PaymentsReceived.Where(item => item.TableBookingId! == req.TableBookingId)
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

        response.SummaryOrderItems = new();

        foreach (var x in response.OrderItems)
        {
            var match = response.SummaryOrderItems.FirstOrDefault(y => AreEntitiesEqual(x, y));

            decimal ItemPrice = x.MenuItem.Price;
            decimal optionsPerItem = x.OrderItemOptions?.Sum(o => o.Option.Price) ?? 0;
            decimal extrasPerItem = x.OrderItemExtras?.Sum(e => e.Extra.Price) ?? 0;

            decimal totalPerItem = ItemPrice + optionsPerItem + extrasPerItem;

            if (match != null)
            {
                match.Quantity += 1;
                match.Total += totalPerItem;
                match.OptionsTotal += optionsPerItem;
                match.ExtrasTotal += extrasPerItem;
            }
            else
            {
                var newItem = new BillOrderItemDTO
                {
                    MenuItemId = x.MenuItemId,
                    MenuItem = x.MenuItem,
                    OrderItemOptions = x.OrderItemOptions?.Where(x => x.Option.Price > 0).ToList() ?? [],
                    OrderItemExtras = x.OrderItemExtras?.Where(x => x.Extra.Price > 0).ToList() ?? [],
                    Quantity = 1,
                    Total = totalPerItem,
                    OptionsTotal = optionsPerItem,
                    ExtrasTotal = extrasPerItem
                };

                response.SummaryOrderItems.Add(newItem);
            }
            var existingDivision = response.Divisions.FirstOrDefault(d => d.DivisionId == x.MenuItem.DivisionId);
            if (existingDivision != null)
            {
                existingDivision.Total += totalPerItem;
            }
            else
            {
                response.Divisions.Add(new DivisionDTO
                {
                    DivisionId = x.MenuItem.DivisionId,
                    FriendlyName = x.MenuItem.Division?.FriendlyName ?? "Unknown",
                    Total = totalPerItem
                });
            }
        }

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

        var paymentStatusIds = _dbContext.OrderItemStatus.Where(x => x.IsBillable).Select(rd => rd.OrderItemStatusId).ToList();
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
    static bool AreEntitiesEqual(BillOrderItemDTO a, BillOrderItemDTO b)
    {
        if (a.MenuItemId != b.MenuItemId)
            return false;

        var aOptionIds = a.OrderItemOptions?.Where(x => x.Option.Price > 0).Select(o => o.OptionId).OrderBy(id => id).ToList() ?? new();
        var bOptionIds = b.OrderItemOptions?.Where(x => x.Option.Price > 0).Select(o => o.OptionId).OrderBy(id => id).ToList() ?? new();

        var aExtraIds = a.OrderItemExtras?.Where(x => x.Extra.Price > 0).Select(e => e.ExtraId).OrderBy(id => id).ToList() ?? new();
        var bExtraIds = b.OrderItemExtras?.Where(x => x.Extra.Price > 0).Select(e => e.ExtraId).OrderBy(id => id).ToList() ?? new();

        return aOptionIds.SequenceEqual(bOptionIds) && aExtraIds.SequenceEqual(bExtraIds);
    }
}