using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Divisions.GetAll;

public class Endpoint : EndpointWithoutRequest<List<Pos.Entities.Division>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/division");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.Division.ToListAsync();
        await SendAsync(results);
    }
}