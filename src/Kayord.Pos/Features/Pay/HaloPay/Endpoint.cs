using System.Runtime.CompilerServices;
using Kayord.Pos.Common.Wrapper;

namespace Kayord.Pos.Features.Pay.HaloPay;

public class Endpoint : Endpoint<Request, Result<GetLink.Response>>
{
    private readonly ILogger<Endpoint> _logger;
    private readonly HaloService _halo;
    private int i = 0;
    private string _reference = string.Empty;

    public Endpoint(HaloService halo, ILogger<Endpoint> logger)
    {
        _halo = halo;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/pay/haloPay/{tableBookingId}/{amount}/{userId}");
        AllowAnonymous();
        Options(x => x.RequireCors(p => p.AllowAnyOrigin()));
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        try
        {
            await SendEventStreamAsync("pay", GetStatus(ct, r), ct);
        }
        catch (Exception)
        {
            await SendErrorsAsync(500);
        }
    }

    private async IAsyncEnumerable<object> GetStatus([EnumeratorCancellation] CancellationToken ct, Request r)
    {
        while (!ct.IsCancellationRequested)
        {
            _logger.LogInformation("Item {i}", i);
            i++;
            if (i > 15)
            {
                throw new TimeoutException("Timeout");
            }

            if (i == 1)
            {
                var results = await _halo.GetLink(r.Amount, r.TableBookingId, r.UserId);
                if (!results.Failure)
                {
                    _reference = results.Value.reference;
                    yield return new { results.Value.reference, results.Value.url, type = "link" };
                }
                else
                {
                    throw new Exception(results.Error);
                }
            }
            var result = await _halo.GetStatus(_reference, r.UserId);
            yield return new { result, type = "status" };
            await Task.Delay(5000);
        }
    }
}