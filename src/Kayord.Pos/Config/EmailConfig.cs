namespace Kayord.Pos.Config;

public class EmailConfig
{
    public string? Host { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int Port { get; set; } = 587;
    public string? Name { get; set; }
}