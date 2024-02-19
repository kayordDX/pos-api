using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Pay.Status;

public class Endpoint : Endpoint<Request, Result<Dto.StatusResultDto>>
{
    private ILogger<Endpoint> _logger;
    private readonly HaloService _halo;
    private readonly CurrentUserService _cu;

    public Endpoint(ILogger<Endpoint> logger, HaloService halo, CurrentUserService cu)
    {
        _logger = logger;
        _halo = halo;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("pay/status/{reference}");
        // AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(_cu.UserId))
        {
            await SendUnauthorizedAsync();
            return;
        }
        var result = await _halo.GetStatus(r.Reference, _cu.UserId);
        await SendAsync(result);
    }
}