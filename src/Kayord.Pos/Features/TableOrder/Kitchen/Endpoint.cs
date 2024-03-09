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
        UserRole? role = await _dbContext.UserRole.FirstOrDefaultAsync(x => x.UserId == _cu.UserId);
        if (role == null)
            await SendNotFoundAsync();
        else
            roleId = role.RoleId;

        var divisionIds = _dbContext.RoleDivision.Where(x => x.RoleId == roleId).Select(rd => rd.DivisionId).ToList();
        var statusIds = _dbContext.OrderItemStatus.Where(x => (x.isBackOffice == role!.isBackOffice || x.isFrontLine == role!.isFrontLine) && x.isComplete != true && x.isCancelled != true).Select(rd => rd.OrderItemStatusId).ToList();
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
            .Where(x => x.SalesPeriod.OutletId == outletId && x.CloseDate == null)
            .ProjectToDto()
            .ToListAsync();

        result.ForEach(dto =>
        {
            dto.OrderItems = dto.OrderItems!
            .Where(oi => statusIds.Contains(oi.OrderItemStatusId) &&
                divisionIds.Contains(oi.MenuItem.DivisionId))
            .ToList();
        });

        if (role!.isBackOffice)
            result = result.Where(x => x.OrderItems!.Any()).Where(x => x.CloseDate == null && x.OrderItems!.Where(y => y.OrderItemStatusId != 1 && y.OrderItemStatusId != 6).Count() > 0).ToList();
        if (role!.isFrontLine)
            result = result.Where(x => x.OrderItems!.Any())
                    .Where(y => y.User.UserId == _cu.UserId && y.CloseDate == null
        && y.OrderItems!.Where(x => x.OrderItemStatusId != 1 && x.OrderItemStatusId != 6).Count() > 0).ToList();

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