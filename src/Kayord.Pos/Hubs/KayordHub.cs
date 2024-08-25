using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Events;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public interface IKayordHub
{
    Task ReceiveMessage(string message);
    Task PayMessage(Result<Features.Pay.Dto.StatusResultDto> request);
    Task Notification(SignalEvent notification);
    Task PlaySound(SoundEvent sound);
}

public class KayordHub : Hub<IKayordHub>
{
    public async Task JoinGroup(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }

    public async Task LeaveGroup(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }
}