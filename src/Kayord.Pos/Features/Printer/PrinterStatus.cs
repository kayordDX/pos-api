using ESCPOS_NET;

namespace Kayord.Pos.Features.Printer;

public class PrinterStatus
{
    public DateTime DateUpdated { get; set; } = DateTime.Now;
    public PrinterConfig PrinterConfig { get; set; } = new();
    public PrinterStatusEventArgs? PrinterStatusEventArgs { get; set; } = null;
}