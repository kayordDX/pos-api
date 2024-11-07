using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill.EmailBill;
using Kayord.Pos.Features.Printer;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Bill.PrintBill
{
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
            Post("/bill/print");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var printer = await _dbContext.Printer.Where(x => x.Id == req.PrinterId).AsNoTracking().FirstOrDefaultAsync();
            if (printer == null)
            {
                await SendAsync(false);
                return;
            }

            PdfRequest pdfRequest = await BillHelper.GetPdfRequestAsync(req.TableBookingId, _dbContext);
            var printInstructions = BillPrint.GetBillPrintInstructions(pdfRequest, printer.LineCharacters);

            PrintMessage printMessage = new()
            {
                PrinterName = printer.PrinterName,
                IPAddress = printer.IPAddress,
                Port = printer.Port,
                PrintInstructions = printInstructions
            };
            await _printService.Print(printer.OutletId, printer.DeviceId, printMessage);
            await SendAsync(true);
        }
    }
}