using Kayord.Pos.Data;
using Kayord.Pos.Features.SalesPeriod.Create;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.SalesPeriod.Get;

public class Endpoint : Endpoint<Request, Pos.Entities.SalesPeriod>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/salesPeriod/{outletId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var result = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.OutletId == r.OutletId && x.StartDate != null && x.EndDate == null);
        if (result == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        await Send.OkAsync(result);
    }
}