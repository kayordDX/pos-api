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
        var results = await _dbContext.Clock.Where(x => x.OutletId == r.OutletId && x.EndDate == null)
            .Include(i => i.Staff)
            .ToListAsync();
        await SendAsync(results);
    }
}