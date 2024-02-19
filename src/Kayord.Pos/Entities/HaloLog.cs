namespace Kayord.Pos.Entities;

public class HaloLog
{
    public long Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? RequestUrl { get; set; }
    public string? Request { get; set; }
    public string? Response { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
}