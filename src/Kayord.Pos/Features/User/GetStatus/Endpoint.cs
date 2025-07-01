using Kayord.Pos.Data;
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
            .Select(x => new { x.Id, x.IsCurrent, x.UserId, OutletId = x.Outlet.Id, OutletName = x.Outlet.Name })
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
        resp.OutletName = userOutlet.OutletName;

        var divisions = await _dbContext.Database.SqlQuery<DivisionDTO>(
        $"""
            select 
                d.division_id id, d.division_name name
            from user_role_outlet uro
            join role_division rd
                on rd.role_id = uro.role_id
            join role r
                on r.role_id = uro.role_id  
            join role_type rt
                on r.role_type_id = rt.id
            join division d
            on d.division_id = rd.division_id
            where uro.outlet_id = {userOutlet.OutletId}
            and uro.user_id = {_cu.UserId}
            and rt.is_back_office = true and rt.is_front_line = false
        """).ToListAsync(ct);
        resp.Divisions = divisions;

        var features = await _dbContext.OutletFeature
            .Where(x => x.OutletId == userOutlet.OutletId)
            .Select(x => x.Feature)
            .ToListAsync(ct);
        resp.Features = features;

        var salesPeriod = await _dbContext.SalesPeriod
            .Where(x => x.OutletId == userOutlet.OutletId && x.StartDate != null && x.EndDate == null)
            .Select(x => new SalesPeriodDTO() { Id = x.Id, Name = x.Name, EndDate = x.EndDate, StartDate = x.StartDate })
            .FirstOrDefaultAsync(ct);

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
            .AnyAsync(x => x.UserId == _cu.UserId && x.OutletId == userOutlet.OutletId && x.EndDate == null);
        resp.ClockedIn = clockInStatus;

        // Check if user has notification. TODO: Make this more generic
        // Get all waiting items for user
        bool hasNotification = await _dbContext.StockAllocateItem.AnyAsync(x => x.StockAllocateItemStatusId == 2 && x.AssignedUserId == _cu.UserId, ct);
        resp.hasNotification = hasNotification;

        await SendAsync(resp);
    }
}
