namespace Kayord.Pos.Features.User.Pin.Get;

public class Response
{
    public string UserId { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public string Pin { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}