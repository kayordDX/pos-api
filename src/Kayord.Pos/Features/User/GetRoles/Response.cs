namespace Kayord.Pos.Features.User.GetRoles;

public class Response
{
    public List<string> Roles { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}