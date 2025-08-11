using System.Text.Json;
using FirebaseAdmin.Messaging;
using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Features.Notification;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Services;

public class NotificationService
{
    private readonly AppDbContext _dbContext;
    private readonly IWebHostEnvironment _web;
    public NotificationService(AppDbContext dbContext, IWebHostEnvironment web)
    {
        _dbContext = dbContext;
        _web = web;
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

    public async Task<bool> SendNotificationAsync(string title, string body, string token, string userId)
    {
        // Do not send notification when not in prod. For now still sending
        // if (!_web.IsProduction())
        // {
        //     var notificationSummary = new NotificationDTO { Title = title, Body = body, UserId = userId };
        //     string? notificationPayload = JsonSerializer.Serialize(notificationSummary);
        //     await LogNotification(token, true, notificationPayload, userId, "Non prod did not notify");
        //     return true;
        // }

        bool result;
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
            // string? payload = JsonSerializer.Serialize(message);
            bool isSuccess = response.Length > 0;
            result = isSuccess;

            var notificationSummary = new NotificationDTO { Title = title, Body = body, UserId = userId };
            string? notificationPayload = JsonSerializer.Serialize(notificationSummary);

            await LogNotification(token, isSuccess, notificationPayload, userId, null);
            if (!isSuccess)
            {
                await CheckToken(userId, token);
            }
        }
        catch (FirebaseMessagingException e)
        {
            result = false;
            if (e.MessagingErrorCode == MessagingErrorCode.Unregistered || e.MessagingErrorCode == MessagingErrorCode.InvalidArgument)
            {
                // Delete unregistered user
                await RemoveUserToken(userId, token);
            }
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
            await LogNotification(token, false, payload, userId, e.Message);
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

    public async Task CheckToken(string userId, string token)
    {
        var notificationLogs = await _dbContext.NotificationLog
            .Where(x => x.UserId == userId)
            .Where(x => x.Token == token)
            .OrderByDescending(x => x.DateInserted)
            .Take(5)
            .ToListAsync();

        if (notificationLogs.Sum(x => x.IsSuccess ? 0 : 1) >= 3)
        {
            var notificationUser = await _dbContext.NotificationUser.Where(x => x.UserId == userId && x.Token == token).FirstOrDefaultAsync();
            if (notificationUser != null)
            {
                _dbContext.NotificationUser.Remove(notificationUser);
                await _dbContext.SaveChangesAsync();
            }
        }
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

    public async Task RemoveUserToken(string userId, string token)
    {
        var notificationUser = await _dbContext.NotificationUser.FindAsync(userId, token);
        if (notificationUser != null)
        {
            _dbContext.NotificationUser.Remove(notificationUser);
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