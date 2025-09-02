using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Outlet.Counter.GetAll;

public class Endpoint : EndpointWithoutRequest<List<OutletCounter>>
{
    private readonly AppDbContext _dbContext;
    private readonly UserService _userService;


    public Endpoint(AppDbContext dbContext, UserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public override void Configure()
    {
        Get("/outlet/counter");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int outletId = await _userService.GetOutletId();
        var entity = await _dbContext.OutletCounter.Where(x => x.OutletId == outletId).ToListAsync(ct);
        await Send.OkAsync(entity);
    }
}