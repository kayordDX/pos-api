using System.Runtime.CompilerServices;

namespace Kayord.Pos.Features.Pay.Status.SSE;

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
        Get("pay/status/sse/{reference}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        await SendEventStreamAsync("status", GetDataStream(r.Reference, ct), ct);
    }

    private async IAsyncEnumerable<Common.Wrapper.Result<Dto.StatusResultDto>> GetDataStream(string reference, [EnumeratorCancellation] CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            var result = await _halo.GetStatus(reference);
            _logger.LogInformation("Running again {reference}", reference);
            yield return result;
            await Task.Delay(3000);
        }
    }
}