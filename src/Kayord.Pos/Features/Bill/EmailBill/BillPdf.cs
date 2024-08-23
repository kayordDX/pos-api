using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Kayord.Pos.Features.Bill.EmailBill;

public class BillPdf
{
    private readonly PdfRequest _pdfRequest;
    public BillPdf(PdfRequest pdfRequest)
    {
        _pdfRequest = pdfRequest;
    }

    public Document Generate()
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20, Unit.Point);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(t => t.FontSize(10).FontColor(Colors.Grey.Darken4));

                page.Header().Element(ComposeHeader);

                page.Content()
                    .PaddingVertical(10, Unit.Point)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        x.Item().Element(ComposeTable);
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

    void ComposeTable(IContainer container)
    {
        var headerStyle = TextStyle.Default.SemiBold();
        static IContainer HeaderStyle(IContainer container) => container.Background(Colors.Grey.Lighten3).PaddingVertical(5).PaddingHorizontal(2);
        static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).PaddingHorizontal(2);
        static IContainer CellStylePlain(IContainer container) => container.PaddingVertical(5).PaddingHorizontal(2);
        static IContainer CellStyleSub(IContainer container) => container.BorderColor(Colors.Grey.Lighten2).PaddingHorizontal(2);
        static IContainer CellStyleLastSub(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingBottom(5).PaddingHorizontal(2);

        container.PaddingTop(10).Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Element(HeaderStyle).Text("Item");
                header.Cell().Element(HeaderStyle).AlignRight().Text("Price").Style(headerStyle);
            });

            foreach (var item in _pdfRequest.Items)
            {
                if (item.Items?.Count > 0)
                {
                    table.Cell().Element(CellStylePlain).Text(item.Name);
                    table.Cell().Element(CellStylePlain).AlignRight().Text($"{item.Price:C}");
                }
                else
                {
                    table.Cell().Element(CellStyle).Text(item.Name);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Price:C}");
                }

                foreach (var subItem in item.Items ?? [])
                {
                    if (item.Items?.Last() != subItem)
                    {
                        table.Cell().Element(CellStyleSub).Text(subItem.Name);
                        table.Cell().Element(CellStyleSub).AlignRight().Text($"{subItem.Price:C}");
                    }
                    else
                    {
                        table.Cell().Element(CellStyleLastSub).Text(subItem.Name);
                        table.Cell().Element(CellStyleLastSub).AlignRight().Text($"{subItem.Price:C}");
                    }
                }
            }

            static IContainer TotalStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);

            table.Cell().Element(TotalStyle).Text("Total").Style(TextStyle.Default.Bold());
            table.Cell().Element(TotalStyle).AlignRight().Text($"{_pdfRequest.Total:C}").Style(TextStyle.Default.Bold());

            table.Cell().Element(TotalStyle).Text("Total Excluding VAT");
            table.Cell().Element(TotalStyle).AlignRight().Text($"{_pdfRequest.TotalExVAT:C}");

            table.Cell().Element(TotalStyle).Text("VAT");
            table.Cell().Element(TotalStyle).AlignRight().Text($"{_pdfRequest.VAT:C}");

            table.Cell().Element(TotalStyle).Text("Payment Received");
            table.Cell().Element(TotalStyle).AlignRight().Text($"{_pdfRequest.PaymentReceived:C}");

            table.Cell().Element(TotalStyle).Text("TIP");
            table.Cell().Element(TotalStyle).AlignRight().Text($"{_pdfRequest.TipAmount:C}");
        });
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
                    text.Span($"{_pdfRequest.OutletName}");
                });

                if (_pdfRequest.Address != null)
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"{_pdfRequest.Address}");
                    });
                }

                if (_pdfRequest.Company != null)
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"{_pdfRequest.Company}");
                    });
                }

                if (_pdfRequest.Registration != null)
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"Reg {_pdfRequest.Registration}");
                    });
                }

                column.Item().Text(text =>
                {
                    text.Span($"VAT no {_pdfRequest.VATNumber:d}");
                });

                column.Item().Text(text =>
                {
                    text.Span($"{_pdfRequest.TableName}");
                });
                column.Item().Text(text =>
                {
                    text.Span($"Waiter {_pdfRequest.Waiter}");
                });
                column.Item().Text(text =>
                {
                    var statusText = _pdfRequest.IsClosed ? "Closed" : "Open";
                    text.Span($"Bill Status {statusText}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Date: ").SemiBold();
                    text.Span($"{_pdfRequest.BillDate:d}");
                });
            });
        });
    }
}