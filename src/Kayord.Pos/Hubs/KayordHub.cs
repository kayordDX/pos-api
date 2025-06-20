using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Events;
using Kayord.Pos.Services;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public interface IKayordHub
{
    Task ReceiveMessage(string message);
    Task PayMessage(Result<Features.Pay.Dto.StatusResultDto> request);
    Task Notification(SignalEvent notification);
    Task PlaySound(SoundEvent sound);
    Task RefreshOutlet(int outletId);
    Task DeviceAuth(DeviceAuthEvent deviceAuthEvent);
}

public class KayordHub : Hub<IKayordHub>
{
    private readonly RedisClient _redisClient;

    public KayordHub(RedisClient redisClient)
    {
        _redisClient = redisClient;
    }

    public async Task JoinGroup(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }

    public async Task LeaveGroup(string group)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }

    // Create OTP and send to client. Save To Redis
    public async Task GetToken()
    {
        string otp = Common.Utils.GenerateOTP();

        TimeSpan expire = TimeSpan.FromMinutes(5);
        DateTime expireDate = DateTime.Now.AddMinutes(5);

        await _redisClient.SetObjectAsync($"auth:{otp}", new DeviceAuthEvent { ExpireDate = expireDate, OTP = otp }, expire);

        await Clients.Caller.DeviceAuth(new DeviceAuthEvent() { OTP = otp, ExpireDate = DateTime.Now.AddMinutes(5) });
        await Groups.AddToGroupAsync(Context.ConnectionId, otp);
    }
}