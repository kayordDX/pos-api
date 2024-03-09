using System.Security.Claims;

namespace Kayord.Pos.Services;

public class CurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string? Expires => _httpContextAccessor.HttpContext?.User?.FindFirstValue("exp");
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("user_id");
    // TODO: Email is empty. Can we remove this?
    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue("email");
    public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue("name");
}
