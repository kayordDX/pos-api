using Kayord.Pos.Features.Business.Create;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Business.Get;

public class Endpoint : Endpoint<Request, Pos.Entities.Business>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/business/{id}");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var result = await _dbContext.Business.FindAsync(r.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }
        await SendAsync(result);
    }
}