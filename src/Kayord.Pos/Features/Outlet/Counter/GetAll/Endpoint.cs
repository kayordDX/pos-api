using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Outlet.Counter.GetAll;

public class Endpoint : Endpoint<Request, List<OutletCounter>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/outlet/counter/{outletId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.OutletCounter.Where(x => x.OutletId == req.OutletId).ToListAsync(ct);
        await Send.OkAsync(entity);
    }
}