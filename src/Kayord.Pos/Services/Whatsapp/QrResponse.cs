namespace Kayord.Pos.Services.Whatsapp;

public class QrResponse
{
    public bool Success { get; set; }
    public string Qr { get; set; } = string.Empty;
}