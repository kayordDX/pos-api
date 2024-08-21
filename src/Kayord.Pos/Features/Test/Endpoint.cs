
using Kayord.Pos.Data;
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
        TableOrder.GetBill.Request request = new() { TableBookingId = 1807 };
        var bill = await TableOrder.GetBill.Bill.Get(new TableOrder.GetBill.Request() { TableBookingId = 1807 }, _dbContext);

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
        document.GeneratePdf("hello.pdf");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await CreateDocument();
        await SendAsync(true);
    }
}