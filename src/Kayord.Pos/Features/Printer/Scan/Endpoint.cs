using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Printer.Scan;

public class Endpoint : Endpoint<Request, bool>
{
    private readonly AppDbContext _dbContext;
    private readonly PrintService _printService;

    public Endpoint(AppDbContext dbContext, PrintService printService)
    {
        _dbContext = dbContext;
        _printService = printService;
    }

    public override void Configure()
    {
        Post("/printer/scan");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var printer = await _dbContext.Printer.Where(x => x.Id == req.PrinterId).AsNoTracking().FirstOrDefaultAsync();
        if (printer == null)
        {
            await Send.OkAsync(false);
            return;
        }

        PrintMessage printMessage = new()
        {
            PrinterName = printer.PrinterName,
            IPAddress = printer.IPAddress,
            Port = printer.Port,
            PrintInstructions = [],
            Action = "nmap"
        };

        await _printService.Print(printer.OutletId, printer.DeviceId, printMessage);
        await Send.OkAsync(true);
    }
}