namespace Kayord.Pos.Features.User.Pin.Login;

public class Request
{
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public string Pin { get; set; } = string.Empty;
}