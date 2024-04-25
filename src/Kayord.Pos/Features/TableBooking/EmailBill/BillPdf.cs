using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Kayord.Pos.Features.TableBooking.EmailBill;

public class BillPdf
{
    private readonly PdfRequest _pdfRequest;
    public BillPdf(PdfRequest pdfRequest)
    {
        _pdfRequest = pdfRequest;
    }

    public Document Generate()
    {
        FontManager.RegisterFontWithCustomName("Roboto", Common.Helper.Fonts.GetFont());
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20, Unit.Point);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(t => t.FontSize(10).FontColor(Colors.Grey.Darken4).FontFamily("Roboto"));

                page.Header().Element(ComposeHeader);

                page.Content()
                    .PaddingVertical(10, Unit.Point)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        x.Item().Column(column =>
                        {
                            column.Item().Height(50);
                            foreach (var item in _pdfRequest.Items)
                            {
                                column.Item().Height(40).Row(row =>
                                {
                                    row.RelativeItem(5)
                                        .BorderTop(1)
                                        .Padding(10)
                                        .Text(item.Name);

                                    row.RelativeItem(2)
                                        .BorderTop(1)
                                        .Padding(10)
                                        .AlignRight()
                                        .Text($"R{item.Price}");
                                });
                                foreach (var subItem in item.Items ?? [])
                                {
                                    column.Item().Height(20).Row(row =>
                                    {
                                        row.RelativeItem(5)
                                            .PaddingLeft(20)
                                            .PaddingRight(10)
                                            .Text(subItem.Name);

                                        row.RelativeItem(2)
                                            .PaddingLeft(10)
                                            .PaddingRight(10)
                                            .AlignRight()
                                            .Text($"R{subItem.Price}");
                                    });
                                }
                            }

                            column.Item().Height(50).BorderTop(1).Row(row =>
                            {
                                row.RelativeItem(5)
                                    .Padding(10)
                                    .Text("Total").FontSize(18);

                                row.RelativeItem(2)
                                    .Padding(10)
                                    .AlignRight()
                                    .Text($"R{_pdfRequest.Total}");
                            });
                            column.Item().Height(40).BorderTop(1).Row(row =>
                            {
                                row.RelativeItem(5)
                                    .Padding(10)
                                    .Text("Total Excluding VAT");
                                row.RelativeItem(2)
                                    .Padding(10)
                                    .AlignRight()
                                    .Text($"R{_pdfRequest.TotalExVAT}");
                            });
                            column.Item().Height(40).BorderTop(1).Row(row =>
                            {
                                row.RelativeItem(5)
                                    .Padding(10)
                                    .Text("VAT");
                                row.RelativeItem(2)
                                    .Padding(10)
                                    .AlignRight()
                                    .Text($"R{_pdfRequest.VAT}");
                            });

                            column.Item().Height(40).BorderTop(1).Row(row =>
                            {
                                row.RelativeItem(5)
                                    .Padding(10)
                                    .Text("Payment Received");
                                row.RelativeItem(2)
                                    .Padding(10)
                                    .AlignRight()
                                    .Text($"R{_pdfRequest.PaymentReceived}");
                            });

                            column.Item().Height(40).BorderTop(1).Row(row =>
                            {
                                row.RelativeItem(5)
                                    .Padding(10)
                                    .Text("Tip");
                                row.RelativeItem(2)
                                    .Padding(10)
                                    .AlignRight()
                                    .Text($"R{_pdfRequest.TipAmount}");
                            });
                        });

                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ").FontSize(8);
                        x.CurrentPageNumber().FontSize(8);
                    });
            });
        });
        return document;
    }

    private void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Grey.Darken3);

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text($"Invoice #{_pdfRequest.TableBookingId}").Style(titleStyle);

                column.Item().Text(text =>
                {
                    text.Span("VAT Number: ").SemiBold();
                    text.Span($"{_pdfRequest.VATNumber:d}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Date: ").SemiBold();
                    text.Span($"{_pdfRequest.BillDate:d}");
                });
            });

            // row.ConstantItem(100).Height(50).Placeholder();
        });
    }
}