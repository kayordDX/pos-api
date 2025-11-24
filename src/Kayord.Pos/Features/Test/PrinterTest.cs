
using System.Diagnostics;
using ESCPOS_NET.Emitters;
using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill;
using Kayord.Pos.Features.Bill.EmailBill;
using Kayord.Pos.Features.Bill.PrintBill;

namespace Kayord.Pos.Features.Test;

public class PrinterTest : EndpointWithoutRequest<bool>
{
    private readonly AppDbContext _dbContext;

    public PrinterTest(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/test/print");
    }

    private static readonly EPSON e = new();

    public override async Task HandleAsync(CancellationToken ct)
    {
        PdfRequest pdfRequest = await BillHelper.GetPdfRequestAsync(78369, _dbContext);
        var printInstructions = BillPrint.GetBillPrintInstructions(pdfRequest, 64);

        var flattenedList = printInstructions.SelectMany(bytes => bytes);
        var bytes = flattenedList.ToArray();

        await File.WriteAllBytesAsync("test.bin", bytes);

        await Send.OkAsync(true);
    }
}