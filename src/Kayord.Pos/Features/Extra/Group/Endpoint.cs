using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Extra.Group;

public class Endpoint : EndpointWithoutRequest<List<ExtraGroupAdminDTO>>
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
        Get("/extra");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var outletId = await Helper.GetUserOutlet(_dbContext, _user.UserId ?? "");

        var shownExtras = await _dbContext.OutletExtraGroup
            .Where(x => x.OutletId == outletId)
            .Select(x => x.ExtraGroupId)
            .ToListAsync(ct);

        var results = await _dbContext.ExtraGroup
            .Where(x => x.OutletId == outletId)
            .ProjectToAdminDto()
            .ToListAsync(ct);

        foreach (var result in results)
        {
            result.IsGlobal = shownExtras.Contains(result.ExtraGroupId);
        }
        await Send.OkAsync(results);
    }
}