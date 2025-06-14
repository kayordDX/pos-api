using Kayord.Pos.Data;
using Kayord.Pos.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Features.Notification.User;

public class Endpoint : Endpoint<Request, bool>
{
    private readonly IHubContext<KayordHub, IKayordHub> _hub;

    public Endpoint(IHubContext<KayordHub, IKayordHub> hub)
    {
        _hub = hub;
    }

    public override void Configure()
    {
        Post("/notification/user");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await _hub.Clients.User(req.UserId).ReceiveMessage(req.Message);
        await _hub.Clients.Group("outlet:1").ReceiveMessage(req.Message);
        await SendAsync(true);
    }
}