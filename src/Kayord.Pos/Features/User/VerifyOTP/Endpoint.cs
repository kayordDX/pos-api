using Kayord.Pos.Events;
using Kayord.Pos.Hubs;
using Kayord.Pos.Services;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Features.User.VerifyOTP;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly RedisClient _redisClient;
    private readonly UserService _userService;
    private readonly IHubContext<KayordHub, IKayordHub> _hub;

    public Endpoint(RedisClient redisClient, IHubContext<KayordHub, IKayordHub> hub, UserService userService)
    {
        _redisClient = redisClient;
        _hub = hub;
        _userService = userService;
    }

    public override void Configure()
    {
        Post("/user/verifyOTP");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var result = await _redisClient.GetObjectAsync<DeviceAuthEvent>($"auth:{req.OTP}");
        if (result == null)
        {
            Response failedResponse = new()
            {
                IsSuccess = false,
                Message = result != null ? null : "Enter a valid code"
            };
            await SendAsync(failedResponse);
            return;
        }
        else
        {
            await _redisClient.DeleteKeyAsync($"auth:{req.OTP}");
            if (_userService.GetCurrentUserService().UserId == null)
            {
                Response failedResponse = new()
                {
                    IsSuccess = false,
                    Message = result != null ? null : "No valid user found"
                };
                await SendAsync(failedResponse);
                return;
            }
            var token = await _userService.GetCustomToken(_userService.GetCurrentUserService().UserId!);

            await _hub.Clients.Group(req.OTP).DeviceAuth(new DeviceAuthEvent() { OTP = req.OTP, Token = token, ExpireDate = DateTime.Now.AddMinutes(5) });
            Response r = new()
            {
                IsSuccess = result != null,
                Message = result != null ? null : "Enter a valid code"
            };
            await SendAsync(r);
        }
    }
}
