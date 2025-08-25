using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill.EmailBill;
using Kayord.Pos.Services.Whatsapp;
using QuestPDF.Fluent;

namespace Kayord.Pos.Features.Bill.WhatsappBill;

public class Endpoint : Endpoint<Request, bool>
{
    private readonly AppDbContext _dbContext;
    private readonly WhatsappService _whatsappService;

    public Endpoint(AppDbContext dbContext, WhatsappService whatsappService)
    {
        _dbContext = dbContext;
        _whatsappService = whatsappService;
    }

    public override void Configure()
    {
        Post("/bill/whatsapp");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        PdfRequest pdfRequest = await BillHelper.GetPdfRequestAsync(req.TableBookingId, _dbContext);
        BillPdf billPdf = new(pdfRequest);
        var document = billPdf.Generate();

        await using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        string base64 = Convert.ToBase64String(stream.ToArray());

        var status = await _whatsappService.GetStatus();
        if (status.Success != true)
        {
            throw new Exception("No whatsapp client found");
        }

        var numberResponse = await _whatsappService.GetNumberId(req.PhoneNumber, req.CountryCode);
        if (numberResponse == null || numberResponse.Success != true || numberResponse.Result == null)
        {
            throw new Exception("Could not get number");
        }

        var chatId = numberResponse.Result._serialized;

        await _whatsappService.SendMessage(new()
        {
            ChatId = chatId,
            Content =
            $"""
            Dear {req.Name},

            Thank you for choosing {pdfRequest.OutletName}.
            We appreciate your recent visit.

            Please find the attached invoice for your reference.

            If you have any questions or need further assistance, feel free to reach out.

            Best regards,
            {pdfRequest.OutletName}
            """
        });

        await _whatsappService.SendFile(new()
        {
            ChatId = chatId,
            Content = new()
            {
                MimeType = "application/pdf",
                Data = base64,
                Filename = $"Invoice-{pdfRequest.TableBookingId}.pdf"
            }
        });
        await Send.OkAsync(true);
    }
}