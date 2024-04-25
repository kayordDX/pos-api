using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.Entities;

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
        UserOutlet? outlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.isCurrent == true);
        var roles = await _dbContext.Role.Where(x => x.UserRole!.Any(s => s.UserId == _cu.UserId)).Select(s => s.RoleId).ToListAsync();

        if (outlet == null)
        {
            await SendNotFoundAsync();
            return;
        }
        List<int> divisionIds = req.DivisionIds?.Split(",")
           .Select(item =>
           {
               int value;
               bool parsed = int.TryParse(item, out value);
               return new
               {
                   parsed = parsed,
                   value = value
               };
           })
           .Where(item => item.parsed)
           .Select(item => item.value)
           .ToList() ?? new List<int>();

        if (divisionIds.Count == 0)
        {
            divisionIds = await _dbContext.RoleDivision
                .Where(x => roles.Contains(x.RoleId))
                .Select(x => x.DivisionId)
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToListAsync();
        }

        var orderItems = _dbContext.OrderItem
            .Where(x => x.TableBooking.Table.Section.OutletId == outlet.OutletId)
            .Where(x => x.OrderGroupId != null)
            .Where(x => x.OrderItemStatus.isBackOffice == !req.Complete)
            .Where(x => x.OrderItemStatus.isComplete == req.Complete)
            .Where(x => x.OrderItemStatus.isCancelled != true)
            .Where(x => divisionIds.Contains(x.MenuItem.DivisionId ?? 0));

        if (orderItems == null)
        {
            await SendNotFoundAsync();
            return;
        }


        var orderItemDTOs = await orderItems.ProjectToDto().ToListAsync();
        var orderGroups = orderItemDTOs
            .GroupBy(x => new { x.OrderGroupId, x.TableBookingId })
            .Select(s => new OrderGroupDTO()
            {
                OrderGroupId = s.Key.OrderGroupId ?? 0,
                TableBooking = s.FirstOrDefault()?.TableBooking,
                OrderItems = s.OrderByDescending(x => x.Priority).ThenBy(x => x.OrderReceived).ToList()
            })
            .ToList();

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