using Kayord.Pos.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Features.Notification.Test;

public class Endpoint : Endpoint<Request, bool>
{
    private readonly IHubContext<KayordHub, IKayordHub> _hub;

    public Endpoint(IHubContext<KayordHub, IKayordHub> hub)
    {
        _hub = hub;
    }

    public override void Configure()
    {
        Post("/notification/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await _hub.Clients.All.ReceiveMessage(req.Message);
        await Send.OkAsync(true);
    }
}