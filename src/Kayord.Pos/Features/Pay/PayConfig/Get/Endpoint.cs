using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Pay.PayConfig.Get;

public class Endpoint : Endpoint<Request, List<Entities.HaloConfig>>
{
    private readonly AppDbContext _dbContext;
    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/pay/config/{outletId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var result = await _dbContext.HaloConfig
            .Where(x => x.OutletId == req.OutletId)
            .OrderByDescending(x => x.Created)
            .ToListAsync(ct);

        await Send.OkAsync(result);
    }
}
