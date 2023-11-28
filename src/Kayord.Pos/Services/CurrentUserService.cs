using System.Security.Claims;

namespace Kayord.Pos.Services;

public class CurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // public IEnumerable<Claim>? Roles => _httpContextAccessor.HttpContext?.User?.FindAll("cognito:groups");
    public string? Expires => _httpContextAccessor.HttpContext?.User?.FindFirstValue("exp");
    public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue("name");
    public int Type => int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("type") ?? "0");
    private int Id => int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("id") ?? "0");
}
