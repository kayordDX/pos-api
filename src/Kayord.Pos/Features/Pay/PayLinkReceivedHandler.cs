using Kayord.Pos.Events;
using Kayord.Pos.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Features.Pay;

public class PayLinkReceivedHandler : IEventHandler<PayLinkReceivedEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;
    private int i = 0;

    public PayLinkReceivedHandler(IServiceScopeFactory scopeFactory, ILogger<PayLinkReceivedHandler> logger)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(PayLinkReceivedEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var hub = scope.Resolve<IHubContext<KayordHub, IKayordHub>>();
        var service = scope.Resolve<HaloService>();

        if (service == null || hub == null)
        {
            throw new Exception("Dependency injection failed");
        }
        while (!ct.IsCancellationRequested)
        {
            var status = await service.GetStatus(eventModel.reference, eventModel.UserId);
            if (i > 5)
            {
                throw new TimeoutException("Timeout");
            }
            i++;

            await hub.Clients.User(eventModel.UserId).PayMessage(status);
            await Task.Delay(5000);
        }
    }
}