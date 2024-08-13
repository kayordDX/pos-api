using Kayord.Pos.Data;
using Kayord.Pos.Features.TableBooking.EmailBill;
using Kayord.Pos.Features.TableOrder.GetBill;
using Kayord.Pos.Services.Whatsapp;
using QuestPDF.Fluent;
namespace Kayord.Pos.Features.Test;

public class WhatsAppTest : EndpointWithoutRequest<Status?>
{
    private readonly WhatsappService _whatsappService;
    private readonly AppDbContext _dbContext;

    public WhatsAppTest(WhatsappService whatsappService, AppDbContext dbContext)
    {
        _whatsappService = whatsappService;
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/test/whatsapp");
        AllowAnonymous();
    }

    private async Task<string> CreateDocument()
    {
        TableOrder.GetBill.Request request = new() { TableBookingId = 1807 };
        var bill = await Bill.Get(new TableOrder.GetBill.Request() { TableBookingId = 1807 }, _dbContext);

        List<Item> items = new();
        foreach (var order in bill.OrderItems)
        {
            List<SubItem> subItems = new();

            foreach (var extra in order.OrderItemExtras ?? [])
            {
                subItems.Add(new SubItem { Name = $"+ {extra.Extra.Name}", Price = extra.Extra.Price });
            }
            foreach (var option in order.OrderItemOptions ?? [])
            {
                subItems.Add(new SubItem { Name = $"> {option.Option.Name}", Price = option.Option.Price });
            }
            items.Add(new Item { Name = order.MenuItem.Name, Price = order.MenuItem.Price, Items = subItems });
        }
        foreach (var adjustment in bill.Adjustments ?? [])
        {
            items.Add(new Item { Name = adjustment.AdjustmentType.Name, Price = adjustment.Amount });
        }

        PdfRequest pdfRequest = new()
        {
            Balance = 0,
            VAT = 23,
            PaymentReceived = 2,
            TableBookingId = 69,
            TipAmount = 10,
            TotalExVAT = 200,
            Total = 240,
            BillDate = DateTime.Now,
            Items = items,
            OutletName = "Test Company Name",
            VATNumber = "234234234234",
            Logo = null,
            Address = "Shop LM 132",
            Company = "Lifestyle at Jessica's Trading Pty Ltd",
            Registration = "Reg 2017/321508/07"
        };
        BillPdf billPdf = new(pdfRequest);
        var document = billPdf.Generate();
        await using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        string base64 = Convert.ToBase64String(stream.ToArray());
        return base64;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var numberIdResponse = await _whatsappService.GetNumberId("0832142611", null);
        if (numberIdResponse == null || numberIdResponse.Result == null) throw new Exception("Could not get number");

        var base64 = await CreateDocument();

        var file = await _whatsappService.SendFile(new()
        {
            ChatId = numberIdResponse.Result._serialized,
            Content = new()
            {
                MimeType = "application/pdf",
                Data = base64,
                Filename = "invoice.pdf"
            }
        });

        var s = await _whatsappService.GetStatus();
        await SendAsync(s);
    }
}