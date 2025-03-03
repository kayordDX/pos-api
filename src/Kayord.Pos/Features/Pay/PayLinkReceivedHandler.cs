using Kayord.Pos.Events;

namespace Kayord.Pos.Features.Pay;

public class PayLinkReceivedHandler : IEventHandler<PayLinkReceivedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;
    public PayLinkReceivedHandler(IServiceScopeFactory scopeFactory, ILogger<PayLinkReceivedHandler> logger)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(PayLinkReceivedEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var service = scope.Resolve<HaloService>();

        if (service == null)
        {
            throw new Exception("Dependency injection failed");
        }
        int i = 0;
        while (!ct.IsCancellationRequested)
        {
            var status = await service.GetStatus(eventModel.reference, eventModel.UserId, eventModel.OutletId);
            if (i > 12)
            {
                throw new TimeoutException("Timeout");
            }
            i++;
            await Task.Delay(10000, ct);
        }
    }
}