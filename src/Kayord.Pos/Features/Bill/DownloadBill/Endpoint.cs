using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill.EmailBill;
using QuestPDF.Fluent;

namespace Kayord.Pos.Features.Bill.DownloadBill;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/bill/download/{tableBookingId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        PdfRequest pdfRequest = await BillHelper.GetPdfRequestAsync(req.TableBookingId, _dbContext);

        BillPdf billPdf = new(pdfRequest);
        var document = billPdf.Generate();

        using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        await SendBytesAsync(stream.ToArray(), "bill.pdf", "application/pdf", cancellation: ct);
        return;
    }
}