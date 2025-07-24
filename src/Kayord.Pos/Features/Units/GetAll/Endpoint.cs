using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Units.GetAll;

public class Endpoint : EndpointWithoutRequest<List<Unit>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/unit");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.Unit.ToListAsync(ct);
        await SendAsync(results);
    }
}



