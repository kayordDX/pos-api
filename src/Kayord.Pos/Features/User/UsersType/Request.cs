namespace Kayord.Pos.Features.User.UsersType;

public class Request
{
    public bool IsFrontLine { get; set; } = true;
    public bool IsBackOffice { get; set; } = false;
}