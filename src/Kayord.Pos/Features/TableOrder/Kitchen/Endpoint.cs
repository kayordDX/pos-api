using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.Entities;


using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.Kitchen;

public class Endpoint : EndpointWithoutRequest<List<TableBookingDTO>>
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
        Get("/kitchen/getOrders");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int roleId = 0;
        int outletId = 0;
        var role = await _dbContext.UserRole.FirstOrDefaultAsync(x => x.UserId == _cu.UserId);
        if (role == null)
            await SendNotFoundAsync();
        else
            roleId = role.RoleId;

        var divisionIds = _dbContext.RoleDivision.Where(x => x.RoleId == roleId).Select(rd => rd.DivisionId).ToList();
        var kitchenStatusIds = _dbContext.OrderItemStatus.Where(x => x.isKitchen == true).Select(rd => rd.OrderItemStatusId).ToList();
        UserOutlet? outlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.isCurrent == true);
        if (outlet == null)
        {
            await SendNotFoundAsync();
        }
        else
        {
            outletId = outlet.OutletId;
        }

        var result = await _dbContext.TableBooking
            .Include(x => x.OrderItems!
                .Where(x => kitchenStatusIds.Contains(x.OrderItemStatusId) && divisionIds.Contains(x.MenuItem.DivisionId) && x.TableBookingId == x.TableBookingId)
            )
           .Where(x => x.SalesPeriod.OutletId == outletId)
        .ProjectToDto().ToListAsync();
        await SendAsync(result);
    }
}