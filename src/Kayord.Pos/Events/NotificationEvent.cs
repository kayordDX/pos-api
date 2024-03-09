namespace Kayord.Pos.Events;

public class NotificationEvent
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Notification { get; set; } = string.Empty;
    public string? JSONContent { get; set; }
    public DateTime DateSent { get; set; } = DateTime.Now;
    public DateTime? DateRead { get; set; }
    public DateTime? DateExpires { get; set; }
}