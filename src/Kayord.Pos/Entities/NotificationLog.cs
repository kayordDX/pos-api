namespace Kayord.Pos.Entities;

public class NotificationLog
{
    public long Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime DateInserted { get; set; } = DateTime.Now;
    public int ChannelId { get; set; }
    public int HttpStatusResponse { get; set; }
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public string? Payload { get; set; }
}