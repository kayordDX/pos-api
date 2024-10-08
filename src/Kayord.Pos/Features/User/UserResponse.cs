namespace Kayord.Pos.Features.User;

public class UserResponse
{
    public bool IsCurrent { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Roles { get; set; } = string.Empty;
}