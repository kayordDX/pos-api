using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Extra.GetAll;

public class Endpoint : EndpointWithoutRequest<List<Pos.Entities.Extra>>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/extra/all");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var outlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.isCurrent);
        if (outlet == null)
        {
            await SendNotFoundAsync();
            return;
        }
        var ExtraGroupIds = await _dbContext.OutletExtraGroup.Where(x => x.OutletId == outlet.Id).Select(x => x.ExtraGroupId).ToListAsync(); ;
        var results = await _dbContext.Extra
        .Where(x => ExtraGroupIds.Contains(x.ExtraGroupId))
        .OrderBy(x => x.Name)
        .ToListAsync();
        await SendAsync(results);
    }
}