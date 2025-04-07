using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Business.GetOutlets;

public class Endpoint : Endpoint<Request, List<Entities.Outlet>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/business/outlets/{outletId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var outlet = await _dbContext.Outlet.Where(x => x.Id == req.OutletId).FirstOrDefaultAsync();
        if (outlet == null)
        {
            await SendNotFoundAsync();
            return;
        }
        var results = await _dbContext.Outlet.Where(x => x.BusinessId == outlet.BusinessId).ToListAsync(ct);
        await SendAsync(results);
    }
}