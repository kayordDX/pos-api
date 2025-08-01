namespace Kayord.Pos.Config;

public class AppConfig
{
    public string EncryptionKey { get; set; } = string.Empty;
    public string EncryptionSalt { get; set; } = string.Empty;
    public string GeminiKey { get; set; } = string.Empty;
    public string? GeminiModel { get; set; }
}