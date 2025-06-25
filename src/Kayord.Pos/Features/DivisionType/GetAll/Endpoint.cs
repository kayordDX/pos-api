using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.DivisionType.GetAll;

public class Endpoint : Endpoint<Request, List<Entities.DivisionType>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/divisionType");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.DivisionType.ToListAsync(ct);
        await SendAsync(results);
    }
}