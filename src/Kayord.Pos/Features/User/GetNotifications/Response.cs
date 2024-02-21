using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.User.GetNotifications;


public class Response
{
    public List<UserNotification>? Notifications { get; set; }
}