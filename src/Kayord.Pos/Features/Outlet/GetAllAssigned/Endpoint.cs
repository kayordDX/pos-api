using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Outlet.GetAllAssigned;

public class Endpoint : EndpointWithoutRequest<List<Pos.Entities.Outlet>>
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
        Get("/outlet/assigned");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.UserRoleOutlet
            .Where(x => x.UserId == _cu.UserId)
            .Include(x => x.Outlet)
            .Select(x => x.Outlet)
            .ToListAsync();
        await SendAsync(results);
    }
}
