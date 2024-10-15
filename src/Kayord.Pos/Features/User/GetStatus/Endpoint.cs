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
        };

        var userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent);
        if (userOutlet == null)
        {
            await SendAsync(resp);
            return;
        }

        var userRoles = await _dbContext.UserRoleOutlet
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == _cu.UserId && ur.OutletId == userOutlet.OutletId)
            .Select(ur => ur.Role!.Name)
            .ToListAsync();

        resp.Roles = userRoles;

        resp.OutletId = userOutlet.OutletId;
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
        await SendAsync(resp);
    }
}
