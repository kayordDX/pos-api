using ESCPOS_NET.Emitters;
using Kayord.Pos.Features.Bill.EmailBill;

namespace Kayord.Pos.Features.Bill.PrintBill;

public static class BillPrint
{
    private static EPSON e = new();
    public static List<byte[]> GetBillPrintInstructions(PdfRequest request)
    {
        List<byte[]> printInstructions = [
            ..Header(request),
            ..Body(request),
            ..Footer(request)
        ];
        return printInstructions;
    }

    private static List<byte[]> Body(PdfRequest request)
    {
        List<byte[]> body = [
           e.SetStyles(PrintStyle.FontB),
            e.FeedLines(3),
            e.LeftAlign(),
         ];

        foreach (var item in request.Items)
        {
            body.Add(PrintColumnLine(item.Name, $"{item.Price:0.00}"));
            foreach (var subItem in item.Items ?? [])
            {
                if (subItem.Price > 0)
                {
                    body.Add(PrintColumnLine(subItem.Name, $"{subItem.Price:0.00}"));
                }
            }
        }
        body.Add(e.FeedLines(3));
        return body;
    }

    private static List<byte[]> Header(PdfRequest request)
    {
        string billStatus = request.IsClosed ? "Closed" : "Open";
        List<byte[]> header = [
            e.CenterAlign(),
            e.PrintLine(""),
           // e.SetBarcodeHeightInDots(360),
           // e.SetBarWidth(BarWidth.Default),
           // e.SetBarLabelPosition(BarLabelPrintPosition.None),
           // e.PrintBarcode(BarcodeType.ITF, request.TableBookingId.ToString().Length > 4 ? request.TableBookingId.ToString().Substring(request.TableBookingId.ToString().Length - 4) : request.TableBookingId.ToString()),
           // e.PrintLine(""),
            e.SetStyles(PrintStyle.Bold),
            e.PrintLine($"Invoice #{request.TableBookingId}"),
            e.SetStyles(PrintStyle.None),
            e.PrintLine(request.OutletName),
            e.PrintLine(request.Address),
            e.PrintLine(request.Company),
            e.PrintLine($"Reg {request.Registration}"),
            e.PrintLine($"VAT no {request.VATNumber}"),
            e.PrintLine(request.TableName),
            e.PrintLine($"Waiter {request.Waiter}"),
            e.PrintLine($"Bill Status {billStatus}"),
            e.PrintLine($"Date: {request.BillDate:d}"),
        ];
        return header;
    }

    private static List<byte[]> Footer(PdfRequest request)
    {
        List<byte[]> footer = [
            e.PrintLine("----------------------------------------------------------------"),
            e.SetStyles(PrintStyle.Bold),
            PrintColumnLine("Total", $"{request.Total:0.00}", 48),
            e.SetStyles(PrintStyle.None),
            PrintColumnLine("Total Excluding VAT", $"{request.TotalExVAT:0.00}", 48),
            PrintColumnLine("VAT", $"{request.VAT:0.00}", 48),
            PrintColumnLine("Payment Received", $"{request.PaymentReceived:0.00}", 48),
            PrintColumnLine("Tip", $"{request.TipAmount:0.00}", 48),
            e.SetStyles(PrintStyle.FontB),
            e.PrintLine("----------------------------------------------------------------"),
            e.FeedLines(5),
            e.FullCut()
        ];
        return footer;
    }

    private static byte[] PrintColumnLine(string left, string right, int maxLength = 64)
    {
        int totalLength = left.Length + right.Length;
        if (right.Length > maxLength)
        {
            left = string.Empty;
            right = right.Substring(0, maxLength);
            totalLength = left.Length + right.Length;
        }

        if (totalLength > maxLength)
        {
            int leftSize = Math.Max(maxLength - (right.Length + 3), 0);
            left = left.Substring(0, leftSize) + "...";
            totalLength = left.Length + right.Length;
        }
        int spaces = maxLength - totalLength;
        string line = left.PadRight(left.Length + spaces) + right;
        return e.PrintLine(line);
    }
}