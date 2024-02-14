namespace Kayord.Pos.Features.Pay.Status;

public class Endpoint : Endpoint<Request>
{
    private ILogger<Endpoint> _logger;
    private readonly HaloService _halo;

    public Endpoint(ILogger<Endpoint> logger, HaloService halo)
    {
        _logger = logger;
        _halo = halo;
    }

    public override void Configure()
    {
        Get("pay/status/{reference}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var result = await _halo.GetStatus(r.Reference);
        await SendAsync(result);
    }
}