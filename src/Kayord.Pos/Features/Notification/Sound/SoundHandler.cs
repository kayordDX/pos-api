using Kayord.Pos.Events;
using Kayord.Pos.Hubs;
using Kayord.Pos.Services;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Features.Notification;

public class SoundHandler : IEventHandler<SoundEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SoundHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(SoundEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var hub = scope.Resolve<IHubContext<KayordHub, IKayordHub>>();

        if (hub == null)
        {
            throw new Exception("Dependency injection failed");
        }

        await hub.Clients.Group($"outlet:{eventModel.OutletId}").PlaySound(eventModel);
    }
}