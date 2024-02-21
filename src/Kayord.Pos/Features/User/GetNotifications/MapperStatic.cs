using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.Features.User.GetNotifications;

[Mapper]
public static partial class MapperStatic
{
    public static partial IQueryable<UserNotificationDTO> ProjectToDto(this IQueryable<Entities.UserNotification> q);

}