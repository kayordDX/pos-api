namespace Kayord.Pos.Features.User.Pin.Create;

public class Request
{
    public string Pin { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}