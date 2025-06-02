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
                        x.Item()
                            .PaddingVertical(10)
                            .LineHorizontal(1)
                            .LineColor(Colors.Black);

                        x.Item().Element(ComposeTable);
                        x.Item().Element(ComposeTableSummary);
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
        static IContainer CellStylePlain(IContainer container) => container.PaddingVertical(5).PaddingHorizontal(2);
        static IContainer CellStylePlainCenter(IContainer container) => container.AlignCenter().PaddingVertical(5).PaddingHorizontal(2);
        static IContainer CellStyleSub(IContainer container) => container.PaddingHorizontal(2);

        container.PaddingTop(10).PaddingBottom(0).Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(20);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            foreach (var item in _pdfRequest.Items)
            {
                table.Cell().Element(CellStylePlainCenter).Text(item.Count.ToString());
                table.Cell().Element(CellStylePlain).Text(item.Name);
                table.Cell().Element(CellStylePlain).AlignRight().Text($"{item.Price:C}").Style(TextStyle.Default.Light());
                table.Cell().Element(CellStylePlain).AlignRight().Text($"{item.TotalPrice:C}");

                foreach (var subItem in item.Items ?? [])
                {
                    table.Cell().Element(CellStyleSub).Text("");
                    table.Cell().Element(CellStyleSub).Text(subItem.Name);
                    table.Cell().Element(CellStyleSub).AlignRight().Text($"{subItem.Price:C}").Style(TextStyle.Default.Light());
                    table.Cell().Element(CellStyleSub).AlignRight().Text("");
                }
            }
        });
    }

    void ComposeTableSummary(IContainer container)
    {
        static IContainer CellStylePlain(IContainer container) => container.PaddingVertical(5).PaddingHorizontal(2);

        container.Column(c =>
        {
            if (_pdfRequest.Divisions.Count > 0)
            {
                c.Item()
                    .PaddingVertical(10)
                    .LineHorizontal(1)
                    .LineColor(Colors.Black);

                c.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    foreach (var division in _pdfRequest.Divisions)
                    {
                        table.Cell().Element(CellStylePlain).Text(division.FriendlyName);
                        table.Cell().Element(CellStylePlain).AlignRight().Text($"{division.Total:C}");
                    }
                });
                c.Item()
                    .PaddingVertical(10)
                    .LineHorizontal(1)
                    .LineColor(Colors.Black);
            }
            else
            {
                c.Item()
                    .PaddingVertical(10);
            }

            c.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    table.Cell().Element(CellStylePlain).Text("Total").Style(TextStyle.Default.Bold());
                    table.Cell().Element(CellStylePlain).AlignRight().Text($"{_pdfRequest.Total:C}").Style(TextStyle.Default.Bold());
                });

                table.Cell().Element(CellStylePlain).Text("Total Excluding VAT");
                table.Cell().Element(CellStylePlain).AlignRight().Text($"{_pdfRequest.TotalExVAT:C}");

                table.Cell().Element(CellStylePlain).Text("VAT");
                table.Cell().Element(CellStylePlain).AlignRight().Text($"{_pdfRequest.VAT:C}");

                table.Cell().Element(CellStylePlain).Text("Payment Received");
                table.Cell().Element(CellStylePlain).AlignRight().Text($"{_pdfRequest.PaymentReceived:C}");

                table.Cell().Element(CellStylePlain).Text("TIP");
                table.Cell().Element(CellStylePlain).AlignRight().Text($"{_pdfRequest.TipAmount:C}");
            });
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