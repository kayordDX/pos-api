using System.IO.Compression;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Staff.GetAllClockedIn;

public class Endpoint : Endpoint<Request, List<Pos.Entities.Clock>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/staff/{OutletId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var salesperiod = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x=>x.OutletId == r.OutletId && x.EndDate != null);
        if(salesperiod == null)
        {
            await SendNotFoundAsync();
            return;
        }
            var results = await _dbContext.Clock.Where(x=>x.SalesPeriodId == salesperiod.Id && x.EndDate == null)
            .Include(i => i.Staff)
            .Include(p=>p.SalesPeriod).ToListAsync();
        await SendAsync(results);
    }
}