using Kayord.Pos.Features.Business.Create;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using Kayord.Pos.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;

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

    private void CreateDocument()
    {
        FontManager.RegisterFontWithCustomName("Roboto", Common.Helper.Fonts.GetFont());
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20).FontFamily("Roboto"));

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
        .GeneratePdf("hello.pdf");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CreateDocument();
        await SendAsync(true);
    }
}