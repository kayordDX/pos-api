using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Division.GetAll;

public class Endpoint : Endpoint<Request, List<Entities.Division>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/stock/division");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.Division.Where(x => x.OutletId == req.OutletId).ToListAsync(ct);
        await SendAsync(results);
    }
}



