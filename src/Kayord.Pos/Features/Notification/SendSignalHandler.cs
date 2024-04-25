using Kayord.Pos.Events;
using Kayord.Pos.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Features.Notification;

public class SendSignalHandler : IEventHandler<SignalEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;

    public SendSignalHandler(IServiceScopeFactory scopeFactory, ILogger<SendNotificationHandler> logger)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(SignalEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var hub = scope.Resolve<IHubContext<KayordHub, IKayordHub>>();

        if (hub == null)
        {
            throw new Exception("Dependency injection failed");
        }

        await hub.Clients.User(eventModel.UserId).Notification(eventModel);
    }
}