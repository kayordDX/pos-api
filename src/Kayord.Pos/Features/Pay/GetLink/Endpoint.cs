using Azure;
using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Data;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Pay.GetLink;

public class Endpoint : Endpoint<Request, Result<Response>>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;
    private readonly HaloService _halo;

    public Endpoint(AppDbContext dbContext, HaloService halo, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _halo = halo;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("/pay/getLink");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(_cu.UserId))
        {
            await SendUnauthorizedAsync();
            return;
        }
        var results = await _halo.GetLink(req.Amount, req.TableBookingId, _cu.UserId);
        await SendAsync(results);
    }
}