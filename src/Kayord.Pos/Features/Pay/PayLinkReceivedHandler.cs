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
        var hub = scope.Resolve<IHubContext<ChatHub>>();
        var service = scope.Resolve<HaloService>();

        if (service == null || hub == null)
        {
            return;
            // TODO: Throw Error Event
        }
        while (!ct.IsCancellationRequested)
        {
            var status = await service.GetStatus(eventModel.reference, eventModel.UserId);
            if (status.Success)
            {
                if (status.Value.ResponseCode == 0 &&
                    status.Value.TransactionId != string.Empty &&
                    status.Value.AuthorisationCode != string.Empty
                )
                {
                    // TODO: Call Payment Event
                }
            }
            if (i > 5)
            {
                throw new TimeoutException("Timeout");

            }
            i++;
            await hub.Clients.All.SendAsync("ReceiveNotification", "message");
            await Task.Delay(5000);
        }
    }
}