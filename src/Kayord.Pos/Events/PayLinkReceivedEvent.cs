namespace Kayord.Pos.Events;

public class PayLinkReceivedEvent
{
    public string url { get; set; } = string.Empty;
    public string reference { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}