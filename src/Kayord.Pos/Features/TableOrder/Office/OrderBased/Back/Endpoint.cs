using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Features.Role;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.TableOrder.Office.OrderBased.Back;

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
        Get("/orderGroup/getOrders");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        UserOutlet? userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent == true);
        if (userOutlet == null)
        {
            await SendNotFoundAsync();
            return;
        }
        List<int> divisionIds = req.DivisionIds != null ? req.DivisionIds.Split(",").Select(int.Parse).ToList() : [];

        var orderItems = _dbContext.OrderItem
            .Where(x => x.TableBooking.Table.Section.OutletId == userOutlet.OutletId)
            .Where(x => x.OrderGroupId != null)
            .Where(x => x.OrderItemStatus.IsBackOffice == !req.Complete)
            .Where(x => x.OrderItemStatus.IsCancelled != true)
            .Where(x => x.OrderItemStatus.IsHistory == req.Complete)
            .Where(x => divisionIds.Contains(x.MenuItem.DivisionId ?? 0));
        if (req.Complete)
        {
            orderItems = orderItems.Where(x => x.TableBooking.CloseDate == null);
        }
        if (orderItems == null)
        {
            await SendNotFoundAsync();
            return;
        }

        var orderItemDTOs = await orderItems.ProjectToDto().ToListAsync();
        var orderGroupQuery = orderItemDTOs
            .GroupBy(x => new { x.OrderGroupId, x.TableBookingId })
            .Select(s => new OrderGroupDTO()
            {
                OrderGroupId = s.Key.OrderGroupId ?? 0,
                LastDate = s.Max(x => x.OrderUpdated),
                Priority = s.Max(x => x.OrderItemStatus.Priority),
                TableBooking = s.FirstOrDefault()?.TableBooking,
                OrderItems = s.ToList()
            });

        if (req.Complete)
        {
            orderGroupQuery = orderGroupQuery
                .OrderByDescending(x => x.Priority).ThenByDescending(x => x.LastDate)
                .ToList();
        }
        else
        {
            orderGroupQuery = orderGroupQuery
                .OrderByDescending(x => x.Priority).ThenBy(x => x.LastDate)
                .ToList();
        }

        var orderGroups = orderGroupQuery.ToList();

        Response r = new()
        {
            LastRefresh = DateTime.UtcNow,
            OrderGroups = orderGroups,
            PendingItems = orderGroups.Sum(x => x.OrderItems?.Count ?? 0),
            PendingOrders = orderGroups.Count()
        };

        await SendAsync(r);
    }
}