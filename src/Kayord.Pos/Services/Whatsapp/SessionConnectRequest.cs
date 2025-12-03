namespace Kayord.Pos.Services.Whatsapp;

public class SessionConnectRequest
{
    public List<string> Subscribe = new() { "Message" };
    public bool Immediate { get; set; } = true;
}