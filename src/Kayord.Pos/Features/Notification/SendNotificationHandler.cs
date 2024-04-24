using Kayord.Pos.Events;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Notification;

public class SendNotificationHandler : IEventHandler<NotificationEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SendNotificationHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(NotificationEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var notificationService = scope.Resolve<NotificationService>();

        if (notificationService == null)
        {
            throw new Exception("Dependency injection failed");
        }

        await notificationService.SendUserNotificationAsync(eventModel.Title, eventModel.Body, eventModel.UserId);
    }
}