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
            ClockedIn = false
        };

        var outlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.isCurrent);

        if (outlet == null)
        {
            await SendAsync(resp);
            return;
        }
        else
        {
            resp.OutletId = outlet.OutletId;
            var salesPeriod = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.OutletId == outlet.OutletId && x.StartDate != null && x.EndDate == null);
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
        }

        var clockInStatus = await _dbContext.Clock.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.OutletId == outlet.OutletId && x.EndDate == null);
        if (clockInStatus == null)
        {
            await SendAsync(resp);
            return;
        }
        await SendAsync(resp);
    }
}
