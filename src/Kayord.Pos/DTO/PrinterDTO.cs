namespace Kayord.Pos.DTO;

public class PrinterDTO
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public int DeviceId { get; set; }
    public string PrinterName { get; set; } = string.Empty;
    public string IPAddress { get; set; } = "10.0.0.3";
    public int Port { get; set; } = 9100;
    public int LineCharacters { get; set; } = 64;
    public bool IsEnabled { get; set; }
    public bool IsConnected { get; set; }
}