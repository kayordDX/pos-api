using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.Entities;


using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.Kitchen;

public class Endpoint : EndpointWithoutRequest<Response>
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
        var kitchenStatusIds = _dbContext.OrderItemStatus.Where(x => x.isBackOffice == true).Select(rd => rd.OrderItemStatusId).ToList();
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
            .Where(x => x.SalesPeriod.OutletId == outletId)
            .ProjectToDto()
            .ToListAsync();

        result.ForEach(dto =>
        {
            dto.OrderItems = dto.OrderItems!
                .Where(oi => kitchenStatusIds.Contains(oi.OrderItemStatusId) &&
                    divisionIds.Contains(oi.MenuItem.DivisionId))
                .ToList();
        });
        result = result.Where(x => x.OrderItems!.Any()).ToList();

        Response response = new()
        {
            LastRefresh = DateTime.Now,
            PendingItems = result.Sum(n => n.OrderItems?.Count) ?? 0,
            PendingTables = result.Count,
            Tables = result
        };
        await SendAsync(response);
    }
}