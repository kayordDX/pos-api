using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Kayord.Pos.Features.Stock;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.SendToKitchen;

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
        Post("/order/sendKitchen");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var tableBooking = await _dbContext.TableBooking
            .Include(x => x.SalesPeriod)
            .Where(x => x.Id == req.TableBookingId).FirstOrDefaultAsync();

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

        var orderItemsToUpdate = await _dbContext.OrderItem
            .Include(x => x.MenuItem)
            .Include(x => x.OrderItemExtras)!
                .ThenInclude(x => x.Extra)
            .Include(x => x.OrderItemOptions)!
                .ThenInclude(x => x.Option)
            .Where(oi => oi.OrderItemStatusId == 1 && oi.TableBookingId == req.TableBookingId)
            .ToListAsync();

        OrderGroup order = new();
        await _dbContext.OrderGroup.AddAsync(order);

        List<int>? divisions = new();
        int outletId = tableBooking?.SalesPeriod.OutletId ?? 0;

        bool isSuccess = true;
        string message = "";


        foreach (var orderItem in orderItemsToUpdate)
        {
            // Check if item has stock
            bool isMenuItemAvailable = await StockManager.IsMenuItemAvailable(orderItem.MenuItemId, _dbContext, ct);
            bool isExtrasAvailable = await StockManager.IsExtrasAvailable(orderItem.OrderItemId, orderItem.MenuItem.DivisionId, _dbContext, ct);
            bool isOptionsAvailable = await StockManager.IsOptionsAvailable(orderItem.OrderItemId, orderItem.MenuItem.DivisionId, _dbContext, ct);

            if (isMenuItemAvailable && isExtrasAvailable && isOptionsAvailable)
            {
                orderItem.OrderItemStatusId = 2;
                orderItem.OrderUpdated = DateTime.UtcNow;
                orderItem.OrderGroup = order;
                var divisionId = orderItem.MenuItem.DivisionId ?? 0;
                if (!divisions.Contains(divisionId))
                {
                    divisions.Add(divisionId);
                }
                await _dbContext.SaveChangesAsync();
                await PublishAsync(new StockEvent() { OrderItemIds = [orderItem.OrderItemId], IsReverse = false }, Mode.WaitForAll);
            }
            else
            {
                List<string> unavailableComponents = new();
                if (!isMenuItemAvailable)
                {
                    unavailableComponents.Add("Menu Item");
                }
                if (!isExtrasAvailable)
                {
                    unavailableComponents.Add("Extras");
                }
                if (!isOptionsAvailable)
                {
                    unavailableComponents.Add("Options");
                }
                // Error message
                isSuccess = false;
                message = $"Remaining items(s) out of stock - {string.Join(", ", unavailableComponents)}";
            }
        }
        await _dbContext.SaveChangesAsync();

        await PublishAsync(new SoundEvent() { OutletId = outletId, DivisionIds = divisions }, Mode.WaitForNone);

        await Send.OkAsync(new Response { IsSuccess = isSuccess, Message = message });
    }
}