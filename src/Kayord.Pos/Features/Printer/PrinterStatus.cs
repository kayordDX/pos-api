using ESCPOS_NET;
using Humanizer;

namespace Kayord.Pos.Features.Printer;

public class PrinterStatus
{
    public DateTime DateUpdated { get; set; } = DateTime.Now;
    public bool IsOutdated => DateTime.UtcNow - DateUpdated > TimeSpan.FromMinutes(10);
    public string DateUpdatedFormatted => DateUpdated.Humanize();
    public PrinterStatusEventArgs? PrinterStatusEventArgs { get; set; } = null;
    public string? LastException { get; set; }
    public int PrinterId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public int LineCharacters { get; set; } = 64;
}