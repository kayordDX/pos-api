using Kayord.Pos.Features.Business.Create;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Kayord.Pos.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using Kayord.Pos.Features.TableBooking.EmailBill;
using Kayord.Pos.Features.TableOrder.GetBill;

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
        TableOrder.GetBill.Request request = new() { TableBookingId = 45 };
        var bill = await Bill.Get(new TableOrder.GetBill.Request() { TableBookingId = 45 }, _dbContext);

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
            Logo = null
        };
        BillPdf billPdf = new(pdfRequest);
        var document = billPdf.Generate();
        document.GeneratePdf("hello.pdf");

        // Document.Create(container =>
        // {
        //     container.Page(page =>
        //     {
        //         page.Size(PageSizes.A4);
        //         page.Margin(2, Unit.Centimetre);
        //         page.PageColor(Colors.White);
        //         page.DefaultTextStyle(x => x.FontSize(20));

        //         page.Header()
        //             .Text("Hello PDF!")
        //             .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

        //         page.Content()
        //             .PaddingVertical(1, Unit.Centimetre)
        //             .Column(x =>
        //             {
        //                 x.Spacing(20);

        //                 x.Item().Text(Placeholders.LoremIpsum());
        //                 x.Item().Image(Placeholders.Image(200, 100));
        //             });

        //         page.Footer()
        //             .AlignCenter()
        //             .Text(x =>
        //             {
        //                 x.Span("Page ");
        //                 x.CurrentPageNumber();
        //             });
        //     });
        // })
        // .GeneratePdf("hello.pdf");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await CreateDocument();
        await SendAsync(true);
    }
}