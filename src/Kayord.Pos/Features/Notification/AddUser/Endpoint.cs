using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Notification.AddUser;

public class Endpoint : Endpoint<Request, bool>
{
    private readonly NotificationService _notificationService;
    private readonly CurrentUserService _user;

    public Endpoint(NotificationService notificationService, CurrentUserService user)
    {
        _notificationService = notificationService;
        _user = user;
    }

    public override void Configure()
    {
        Post("/notification/addUser");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            if (_user.UserId is null)
            {
                await SendAsync(false);
                return;
            }

            await _notificationService.SaveUserToken(_user.UserId, req.Token);
            await SendAsync(true);
        }
        catch (Exception)
        {
            await SendAsync(false);
        }
    }
}