using Kayord.Pos.Data;
using Kayord.Pos.Features.Role;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.GetStatus;

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
        Get("/user/getStatus");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response resp = new()
        {
            OutletId = 0,
            SalesPeriodId = 0,
            ClockedIn = false,
            StatusId = 0,
        };

        var userOutlet = await _dbContext.UserOutlet
            .Include(x => x.Outlet)
            .FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent);

        if (userOutlet == null)
        {
            var userOutlets = await _dbContext.UserOutlet.AnyAsync(x => x.UserId == _cu.UserId, ct);
            if (userOutlets == true)
            {
                var userRole = await _dbContext.UserRoleOutlet.AnyAsync(x => x.UserId == _cu.UserId, ct);
                resp.StatusId = (userRole == true) ? 2 : 1;
            }
            await SendAsync(resp);
            return;
        }

        resp.StatusId = 3;

        var userRoles = await _dbContext.UserRoleOutlet
            .Include(ur => ur.Role!).ThenInclude(x => x.RoleType)
            .Where(ur => ur.UserId == _cu.UserId && ur.OutletId == userOutlet.OutletId)
            .Select(ur => new RoleDTO
            {
                Id = ur.RoleId,
                RoleName = ur.Role!.Name,
                RoleType = ur.Role.RoleType.Name
            })
            .ToListAsync();

        resp.Roles = userRoles;

        resp.OutletId = userOutlet.OutletId;
        resp.OutletName = userOutlet.Outlet.Name;
        var salesPeriod = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.OutletId == userOutlet.OutletId && x.StartDate != null && x.EndDate == null);
        if (salesPeriod == null)
        {
            await SendAsync(resp);
            return;
        }
        else
        {
            resp.SalesPeriod = salesPeriod;
            resp.SalesPeriodId = salesPeriod.Id;
        }


        var clockInStatus = await _dbContext.Clock
            .FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.OutletId == userOutlet.OutletId && x.EndDate == null);
        resp.ClockedIn = clockInStatus != null;

        // Check if user has notification. TODO: Make this more generic
        // Get all waiting items for user
        bool hasNotification = await _dbContext.StockAllocateItem.AnyAsync(x => x.StockAllocateItemStatusId == 2 && x.AssignedUserId == _cu.UserId, ct);
        resp.hasNotification = hasNotification;

        await SendAsync(resp);
    }
}
