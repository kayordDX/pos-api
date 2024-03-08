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
        await _hub.Clients.User("100308736810173424324").ReceiveMessage("Hardcoded");
        await _hub.Clients.User(req.UserId).ReceiveMessage(req.Message);
        await _hub.Clients.User(req.UserId).PayMessage(req);
        // await _hub.Clients.Group("monkeys").ReceiveMessage(req.Message);
        await SendAsync(true);
    }
}