using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Extra.GetAll;

public class Endpoint : EndpointWithoutRequest<List<Pos.Entities.Extra>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/extra/all");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.Extra.OrderBy(x => x.PositionId).ToListAsync();
        await SendAsync(results);
    }
}