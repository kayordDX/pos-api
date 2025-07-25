using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Option.Group;

public class Endpoint : EndpointWithoutRequest<List<OptionGroupBasicDTO>>
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
        Get("/option");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var outletId = await Helper.GetUserOutlet(_dbContext, _user.UserId ?? "");

        var results = await _dbContext.OptionGroup
            .Where(x => x.OutletId == outletId)
            .ProjectToBasicDto()
            .ToListAsync(ct);

        await Send.OkAsync(results);
    }
}