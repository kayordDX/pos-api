using Azure;
using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Pay.GetLink;

public class Endpoint : Endpoint<Request, Result<Response>>
{
    private readonly AppDbContext _dbContext;
    private readonly HaloService _halo;

    public Endpoint(AppDbContext dbContext, HaloService halo)
    {
        _dbContext = dbContext;
        _halo = halo;
    }

    public override void Configure()
    {
        Get("/pay/getLink");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Section.FindAsync(1);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }
        var results = await _halo.GetLink(req.Amount);
        await SendAsync(results);
    }
}