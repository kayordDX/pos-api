using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Kayord.Pos.Hubs;
using Kayord.Pos.Services;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Features.User.LinkAccount;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly RedisClient _redisClient;
    private readonly UserService _userService;
    private readonly IHubContext<KayordHub, IKayordHub> _hub;
    private readonly AppDbContext _dbContext;

    public Endpoint(RedisClient redisClient, IHubContext<KayordHub, IKayordHub> hub, UserService userService, AppDbContext dbContext)
    {
        _redisClient = redisClient;
        _hub = hub;
        _userService = userService;
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/user/linkAccount");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var result = await _redisClient.GetObjectAsync<DeviceAuthEvent>($"auth:{req.OTP}");
        Audit audit = new Audit()
        {
            AuditTypeId = 2,
            UserId = _userService.GetCurrentUserService().UserId,
            Detail = ""
        };

        if (result == null)
        {
            Response failedResponse = new()
            {
                IsSuccess = false,
                Message = result != null ? null : "Enter a valid code"
            };
            audit.Detail = failedResponse.Message;
            await _dbContext.Audit.AddAsync(audit);
            await _dbContext.SaveChangesAsync(ct);
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
                audit.Detail = failedResponse.Message;
                await _dbContext.Audit.AddAsync(audit);
                await _dbContext.SaveChangesAsync(ct);
                await SendAsync(failedResponse);
                return;
            }
            var token = await _userService.GetCustomToken(_userService.GetCurrentUserService().UserId!);

            await _hub.Clients.Group(req.OTP).DeviceAuth(new DeviceAuthEvent() { OTP = req.OTP, Token = token, ExpireDate = DateTime.Now.AddMinutes(5) });
            Response r = new()
            {
                IsSuccess = true,
                Message = null
            };
            audit.AuditTypeId = 1;
            await _dbContext.Audit.AddAsync(audit);
            await _dbContext.SaveChangesAsync(ct);
            await SendAsync(r);
        }
    }
}
