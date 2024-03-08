using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public interface IKayordHub
{
    Task ReceiveMessage(string message);
    Task PayMessage(Features.Notification.User.Request request);
}

public class KayordHub : Hub<IKayordHub>
{

}