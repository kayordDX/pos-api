using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Extra.Items;

public class Endpoint : Endpoint<Request, List<ExtraDTO>>
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
        Get("/extra/{id}");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var outletId = await Helper.GetUserOutlet(_dbContext, _user.UserId ?? "");

        var results = await _dbContext.Extra
            .Where(x => x.OutletId == outletId && x.ExtraGroupId == r.Id)
            .ProjectToDto()
            .ToListAsync(ct);

        await SendAsync(results);
    }
}