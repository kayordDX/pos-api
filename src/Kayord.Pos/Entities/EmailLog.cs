namespace Kayord.Pos.Entities;

public class EmailLog
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Subject { get; set; }
    public string? Message { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public bool IsSent { get; set; } = false;
}