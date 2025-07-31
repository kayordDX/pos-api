namespace Kayord.Pos.Features.Printer;

public class PrintMessage
{
    public string? Action { get; set; }
    public string PrinterName { get; set; } = string.Empty;
    public string IPAddress { get; set; } = "10.0.0.3";
    public int Port { get; set; } = 9100;
    public List<byte[]> PrintInstructions { get; set; } = [];
}