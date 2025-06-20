namespace Kayord.Pos.Features.User.Validate;

public class Request
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Image { get; set; }
    public string Name { get; set; } = string.Empty;
}
