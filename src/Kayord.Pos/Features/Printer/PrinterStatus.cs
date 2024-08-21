using ESCPOS_NET;
using Humanizer;

namespace Kayord.Pos.Features.Printer;

public class PrinterStatus
{
    public DateTime DateUpdated { get; set; } = DateTime.Now;
    public bool IsOutdated => DateTime.Now - DateUpdated > TimeSpan.FromMinutes(10);
    public string DateUpdatedFormatted => DateUpdated.Humanize();
    public PrinterConfig PrinterConfig { get; set; } = new();
    public PrinterStatusEventArgs? PrinterStatusEventArgs { get; set; } = null;
}