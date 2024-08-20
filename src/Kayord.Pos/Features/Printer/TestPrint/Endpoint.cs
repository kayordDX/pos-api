using ESCPOS_NET.Emitters;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Printer.TestPrint
{
    public class Endpoint : Endpoint<Request, bool>
    {
        private readonly PrintService _printService;

        public Endpoint(PrintService printService)
        {
            _printService = printService;
        }

        public override void Configure()
        {
            Get("/printer/test/{outletId}/{printerId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request r, CancellationToken ct)
        {
            EPSON e = new();
            List<byte[]> printInstructions = [
                e.CenterAlign(),
                e.PrintLine(""),
                e.SetBarcodeHeightInDots(360),
                e.SetBarWidth(BarWidth.Default),
                e.SetBarLabelPosition(BarLabelPrintPosition.None),
                e.PrintBarcode(BarcodeType.ITF, "0123456789"),
                e.PrintLine(""),

                e.SetStyles(PrintStyle.Bold),
                e.PrintLine("Invoice #1807"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine("Jessica's Clearwater Mall"),
                e.PrintLine("Shop LM 132"),
                e.PrintLine("Lifestyle at Jessica's trading Pty Ltd"),
                e.PrintLine("Reg 2017/321508/07"),
                e.PrintLine("VAT no 4500281359"),
                e.PrintLine("Date: 2024/07/03"),
                e.SetStyles(PrintStyle.FontB),
                e.FeedLines(3),
                e.LeftAlign(),
                e.PrintLine("Special Cappuccino                                         25.00"),
                e.PrintLine("> No Milk                                                  00.00"),
                e.PrintLine(""),
                e.PrintLine("Mon to Fri - The Real Breakfast                            49.00"),
                e.PrintLine("> Low GI                                                   00.00"),
                e.PrintLine("> Hard                                                     00.00"),
                e.FeedLines(3),
                e.PrintLine("----------------------------------------------------------------"),
                e.SetStyles(PrintStyle.Bold),
                e.RightAlign(),
                e.PrintLine("Total         74.00"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine("Total Excluding VAT:         64.35"),
                e.PrintLine("VAT:         9.65"),
                e.PrintLine("Payment Received:         84.00"),
                e.PrintLine("Tip:         10.00"),
                e.PrintLine("----------------------------------------------------------------"),
                e.FeedLines(5),
                e.FullCut()
                ];
            await _printService.Print(printInstructions, r.OutletId, r.PrinterId);
            await SendAsync(true);
        }
    }
}