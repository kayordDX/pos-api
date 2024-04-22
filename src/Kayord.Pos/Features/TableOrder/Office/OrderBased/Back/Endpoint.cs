using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.Entities;

using Microsoft.EntityFrameworkCore;


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

        var statusIds = _dbContext.OrderItemStatus.Where(x => x.isBackOffice && x.isComplete != true && x.isCancelled != true).Select(rd => rd.OrderItemStatusId).ToList();
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


        var orderGroups = await _dbContext.OrderGroup.FromSql($"""
                SELECT "OrderGroupId" FROM "get_orders_for_outlet"({outletId},{req.DivisionIds})
                """)
                .ProjectToDto().ToListAsync();

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