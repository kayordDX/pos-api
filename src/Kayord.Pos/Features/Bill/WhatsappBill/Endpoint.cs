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

        string phone = _whatsappService.GetNumberWithCountryCode(req.PhoneNumber, req.CountryCode);

        static string ConcatDataUri(string base64)
        {
            const string prefix = "data:application/octet-stream;base64,";
            var buffer = new char[prefix.Length + base64.Length];
            prefix.AsSpan().CopyTo(buffer);
            base64.AsSpan().CopyTo(buffer.AsSpan(prefix.Length));
            return new string(buffer);
        }

        var textResponse = await _whatsappService.SendText(new()
        {
            Phone = phone,
            Body =
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

        if (textResponse.Success)
        {
            await _whatsappService.SendDocument(new()
            {
                Phone = phone,
                FileName = $"Invoice-{pdfRequest.TableBookingId}.pdf",
                Document = ConcatDataUri(base64)
            });
        }
        else
        {
            await Send.OkAsync(false);
        }

        await Send.OkAsync(true);
    }
}