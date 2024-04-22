using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.Entities;

using Microsoft.EntityFrameworkCore;
using Kayord.Pos.DTO;


namespace Kayord.Pos.Features.Order.BackOffice;

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
        int outletId = 0;
        UserOutlet? outlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.isCurrent == true);
        if (outlet == null)
        {
            await SendNotFoundAsync();
            return;
        }
        else
        {
            outletId = outlet.OutletId;
        }

        var orderItems = _dbContext.OrderItem
            .Where(x => x.TableBooking.Table.Section.OutletId == 1)
            .Where(x => x.OrderGroupId != null);
        // .Where(x => x.OrderItemStatus.isBackOffice == true)
        // .Where(x => x.OrderItemStatus.isComplete != true)
        // .Where(x => x.OrderItemStatus.isCancelled != true)
        // .Where(x => x.MenuItem.DivisionId == 1);

        if (orderItems == null)
        {
            await SendNotFoundAsync();
            return;
        }


        var orderItemDTOs = await orderItems.ProjectToDto().ToListAsync();
        var orderGroups = orderItemDTOs
            .GroupBy(x => x.OrderGroupId)
            .Select(s => new OrderGroupDTO()
            {
                OrderGroupId = s.Key ?? 0,
                OrderItems = s.ToList()
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