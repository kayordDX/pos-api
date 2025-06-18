using Kayord.Pos.Events;
using Kayord.Pos.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Features.Notification.Sound;

public class Endpoint : EndpointWithoutRequest<bool>
{

    public Endpoint()
    {
    }

    public override void Configure()
    {
        Post("/notification/sound");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await PublishAsync(new SoundEvent() { OutletId = 1, DivisionIds = [2] });
        await SendAsync(true);
    }
}