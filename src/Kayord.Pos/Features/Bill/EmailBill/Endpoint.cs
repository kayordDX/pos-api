using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill.DownloadBill;
using Kayord.Pos.Services;
using MimeKit;
using QuestPDF.Fluent;

namespace Kayord.Pos.Features.Bill.EmailBill;

public class Endpoint : Endpoint<Request, bool>
{
    private readonly AppDbContext _dbContext;
    private readonly IEmailSender _emailSender;

    public Endpoint(AppDbContext dbContext, CurrentUserService user, IEmailSender emailSender)
    {
        _dbContext = dbContext;
        _emailSender = emailSender;
    }

    public override void Configure()
    {
        Post("/bill/email");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        PdfRequest pdfRequest = await BillHelper.GetPdfRequestAsync(req.TableBookingId, _dbContext);
        BillPdf billPdf = new(pdfRequest);
        var document = billPdf.Generate();

        await using var stream = new MemoryStream();
        document.GeneratePdf(stream);

        AttachmentCollection attachment = new()
        {
            { $"Invoice{pdfRequest.TableBookingId}.pdf", stream.ToArray() }
        };

        await _emailSender.SendEmailAsync(req.Email, req.Name, $"{pdfRequest.OutletName} Invoice #{pdfRequest.TableBookingId} {pdfRequest.BillDate}",
        $"""
        Dear {req.Name},

        Thank you for choosing {pdfRequest.OutletName}.
        We appreciate your recent visit.

        Please find the attached invoice for your reference.

        If you have any questions or need further assistance, feel free to reach out.

        Best regards,
        {pdfRequest.OutletName}
        """, attachment);

        // Send Email
        await SendAsync(true);
    }
}