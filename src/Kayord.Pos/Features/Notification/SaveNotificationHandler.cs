using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;

namespace Kayord.Pos.Features.Notification;

public class SaveNotificationHandler : IEventHandler<NotificationEvent>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;

    public SaveNotificationHandler(IServiceScopeFactory scopeFactory, ILogger<SaveNotificationHandler> logger)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task HandleAsync(NotificationEvent eventModel, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var _dbContext = scope.Resolve<AppDbContext>();

        if (_dbContext == null)
        {
            throw new Exception("Dependency injection failed");
        }

        await _dbContext.AddAsync(new UserNotification()
        {
            UserId = eventModel.UserId,
            Notification = eventModel.Notification,
            DateSent = eventModel.DateSent,
            DateExpires = eventModel.DateExpires,
            JSONContent = eventModel.JSONContent,
        });
        await _dbContext.SaveChangesAsync();
    }
}