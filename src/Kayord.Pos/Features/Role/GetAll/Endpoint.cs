using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Role.GetAll;

public class Endpoint : Endpoint<Request, List<Entities.Role>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/role/{outletId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var roles = await _dbContext.Role.Where(x => x.OutletId == req.OutletId).Include(i => i.RoleType).ToListAsync();
        await Send.OkAsync(roles);
    }
}