
using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill;
using Kayord.Pos.Features.Bill.EmailBill;
using Kayord.Pos.Services;
using QuestPDF.Fluent;

namespace Kayord.Pos.Features.Test;

public class Endpoint : EndpointWithoutRequest<bool>
{
    private readonly IEmailSender _emailSender;
    private readonly AppDbContext _dbContext;

    public Endpoint(IEmailSender emailSender, AppDbContext dbContext)
    {
        _emailSender = emailSender;
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    private async Task CreateDocument()
    {
        PdfRequest pdfRequest = await BillHelper.GetPdfRequestAsync(78369, _dbContext);
        BillPdf billPdf = new(pdfRequest);
        var document = billPdf.Generate();
        document.GeneratePdf("hello.pdf");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await CreateDocument();
        await SendAsync(true);
    }
}