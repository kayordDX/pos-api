using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill.EmailBill;
using Kayord.Pos.Services;

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
            PdfRequest pdfRequest = await BillHelper.GetPdfRequestAsync(req.TableBookingId, _dbContext);
            var printInstructions = BillPrint.GetBillPrintInstructions(pdfRequest, req.LineCharacters);
            await _printService.Print(printInstructions, req.OutletId, req.PrinterId);
            await SendAsync(true);
        }
    }
}