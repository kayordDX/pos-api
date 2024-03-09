using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Events;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public interface IKayordHub
{
    Task ReceiveMessage(string message);
    Task PayMessage(Result<Features.Pay.Dto.StatusResultDto> request);
    Task Notification(NotificationEvent notification);
}

public class KayordHub : Hub<IKayordHub>
{

}