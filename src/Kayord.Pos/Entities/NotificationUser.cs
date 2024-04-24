namespace Kayord.Pos.Entities;

public class NotificationUser
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime DateInserted { get; set; } = DateTime.Now;
}