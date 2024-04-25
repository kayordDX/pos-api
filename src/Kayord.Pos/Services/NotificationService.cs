using System.Text.Json;
using FirebaseAdmin.Messaging;
using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Services;

public class NotificationService
{
    private readonly AppDbContext _dbContext;
    public NotificationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendUserNotificationAsync(string title, string body, string userId)
    {
        var notificationUser = await _dbContext.NotificationUser.Where(x => x.UserId == userId).ToListAsync();
        if (notificationUser != null)
        {
            foreach (var item in notificationUser)
            {
                await SendNotificationAsync(title, body, item.Token, userId);
            }
        }
    }

    public async Task<bool> SendNotificationAsync(string title, string body, string token, string? userId)
    {
        bool result = false;
        try
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body,
                    ImageUrl = "https://pos.kayord.com/logo.svg"
                },
                Webpush = new WebpushConfig
                {
                    Notification = new WebpushNotification
                    {
                        Title = title,
                        Body = body,
                        Icon = "https://pos.kayord.com/logo.svg",
                        Vibrate = [100, 200, 100, 200, 400]
                    }
                },
                Android = new AndroidConfig
                {
                    CollapseKey = "Kayord",
                    Notification = new AndroidNotification
                    {
                        Title = title,
                        Body = body,
                        Icon = "https://pos.kayord.com/logo.svg",
                        VibrateTimingsMillis = [100, 200, 100, 200, 400]
                    }
                },
                Token = token
            };
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            string? payload = JsonSerializer.Serialize(message);
            bool isSuccess = response.Length > 0;
            result = isSuccess;
            await LogNotification(token, isSuccess, payload, userId, null);
        }
        catch (Exception ex)
        {
            result = false;
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body,
                    ImageUrl = "https://pos.kayord.com/logo.svg"
                },
                Token = token
            };
            string? payload = JsonSerializer.Serialize(message);
            await LogNotification(token, false, payload, userId, ex.Message);
        }
        return result;
    }

    public async Task SaveUserToken(string userId, string token)
    {
        var notificationUser = await _dbContext.NotificationUser.FindAsync(userId, token);
        if (notificationUser == null)
        {
            await _dbContext.NotificationUser.AddAsync(new NotificationUser()
            {
                Token = token,
                UserId = userId
            });
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task LogNotification(string token, bool isSuccess, string payload, string? userId, string? error)
    {
        await _dbContext.NotificationLog.AddAsync(new NotificationLog()
        {
            UserId = userId ?? "",
            Token = token,
            IsSuccess = isSuccess,
            Error = error,
            Payload = payload
        });
        await _dbContext.SaveChangesAsync();
    }
}