using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Adjustment.GetAll;

public class Endpoint : Endpoint<Request, List<Entities.AdjustmentType>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/adjustment/{outletId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.AdjustmentTypeOutlet
            .Where(x => x.OutletId == req.OutletId)
            .Include(x => x.AdjustmentType)
            .Select(x => x.AdjustmentType)
            .ToListAsync();

        await Send.OkAsync(results);
    }
}
