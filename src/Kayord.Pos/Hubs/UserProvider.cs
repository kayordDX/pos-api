using Kayord.Pos.Services;
using Microsoft.AspNetCore.SignalR;

namespace Kayord.Pos.Hubs;

public class UserProvider : IUserIdProvider
{
    private readonly CurrentUserService _cu;

    public UserProvider(CurrentUserService cu)
    {
        _cu = cu;
    }

    public string? GetUserId(HubConnectionContext connection)
    {
        return _cu.UserId;
    }
}