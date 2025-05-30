using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.AdjustmentTypeOutlet.Remove;

public class Endpoint : Endpoint<Request, Entities.AdjustmentTypeOutlet>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/remove");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.AdjustmentTypeOutlet? entity = await _dbContext.AdjustmentTypeOutlet.FirstOrDefaultAsync(x => x.AdjustmentTypeId == req.AdjustmentTypeId && x.OutletId == req.OutletId);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        _dbContext.AdjustmentTypeOutlet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}
