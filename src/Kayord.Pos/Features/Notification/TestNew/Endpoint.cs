using Kayord.Pos.Events;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Notification.TestNew;

public class Endpoint : Endpoint<Request, bool>
{
    private readonly NotificationService _notificationService;

    public Endpoint(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public override void Configure()
    {
        Post("/notification/testNew");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await PublishAsync(new NotificationEvent()
        {
            UserId = req.UserId,
            Title = req.Title,
            Body = req.Body
        }, Mode.WaitForNone);
        await Send.OkAsync(true);
    }
}