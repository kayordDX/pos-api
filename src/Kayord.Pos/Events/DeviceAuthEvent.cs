namespace Kayord.Pos.Events;

public class DeviceAuthEvent
{
    public string OTP { get; set; } = string.Empty;
    public string? Token { get; set; }
    public DateTime ExpireDate { get; set; }
}