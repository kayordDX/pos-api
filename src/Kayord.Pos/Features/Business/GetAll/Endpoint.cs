using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Business.GetAll;

public class Endpoint : EndpointWithoutRequest<List<Pos.Entities.Business>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/business");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.Business.ToListAsync();
        await Send.OkAsync(results);
    }
}