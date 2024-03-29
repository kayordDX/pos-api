using Kayord.Pos.Features.Business.Create;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Kayord.Pos.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Kayord.Pos.Features.Test;

public class Endpoint : EndpointWithoutRequest<bool>
{
    private readonly IEmailSender _emailSender;

    public Endpoint(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    private byte[] CreateDocument()
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text("Hello PDF!")
                    .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        x.Item().Text(Placeholders.LoremIpsum());
                        x.Item().Image(Placeholders.Image(200, 100));
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        })
        .GeneratePdf();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        MimeKit.AttachmentCollection attachments = new()
        {
            { "test.pdf", CreateDocument() }
        };

        await _emailSender.SendEmailAsync("kokjaco2@gmail.com", "Jaco Kok", "Invoice 2024-03-27", """
        Hi Jaco,
        Please find the attached invoice for today. Here we go again.

        Thank you for your business!
        """, attachments);
        await SendAsync(true);
    }
}