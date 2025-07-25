using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.UpdateGroupOrder;

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
        Post("/order/updateOrderGroup");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        OrderItemStatus? orderItemStatus = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);

        List<int> divisionIds = req.DivisionIds != null ? req.DivisionIds.Split(",").Select(int.Parse).ToList() : [];

        var orderItems = await _dbContext.OrderItem
            .Include(x => x.TableBooking)
                .ThenInclude(b => b.Table)
            .Include(x => x.MenuItem)
                .ThenInclude(x => x.Division)
            .Where(x => x.OrderGroupId == req.OrderGroupId)
            .Where(x => divisionIds.Contains(x.MenuItem.DivisionId ?? 0))
            .ToListAsync(ct);

        // Stock
        if (orderItemStatus?.IsUpdateStock ?? false)
        {
            await PublishAsync(new StockEvent() { OrderItemIds = orderItems.Select(x => x.OrderItemId).ToList(), IsReverse = false }, Mode.WaitForNone);
        }

        bool notify = orderItemStatus?.IsNotify ?? false;
        NotificationEvent notification = new();
        foreach (var item in orderItems)
        {
            notification.Title = $"{item.MenuItem.Division?.DivisionName ?? "Order"} ready";
            notification.Body = $"{item.MenuItem.Division?.DivisionName ?? "Order"} #{req.OrderGroupId} - {item.TableBooking.Table.Name}";
            notification.UserId = item.TableBooking.UserId;
            item.OrderItemStatusId = req.OrderItemStatusId;
        }

        if (notify)
        {
            await PublishAsync(notification, Mode.WaitForNone);
        }

        await _dbContext.SaveChangesAsync();
        await Send.OkAsync(new Response() { IsSuccess = true });
    }
}