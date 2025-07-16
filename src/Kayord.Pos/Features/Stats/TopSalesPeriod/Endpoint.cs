using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stats.TopSalesPeriod;

public class Endpoint : Endpoint<Request, List<Response>>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _user;

    public Endpoint(AppDbContext dbContext, CurrentUserService user)
    {
        _dbContext = dbContext;
        _user = user;
    }

    public override void Configure()
    {
        Get("/stats/salesPeriod");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _user.UserId && x.IsCurrent);
        if (userOutlet == null)
        {
            await SendNotFoundAsync();
            return;
        }
        var result = await _dbContext.SalesPeriod
            .Where(x => x.OutletId == userOutlet.OutletId)
            .Select(x => new Response()
            {
                Id = x.Id,
                Name = x.Name,
                EndDate = x.EndDate,
                StartDate = x.StartDate
            })
            .OrderByDescending(x => x.StartDate)
            .Take(r.Top).ToListAsync(ct);

        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }
        await SendAsync(result);
    }
}