using ESCPOS_NET.Emitters;
using Kayord.Pos.Features.Bill.EmailBill;

namespace Kayord.Pos.Features.Bill.PrintBill;

public static class BillPrint
{
    private static EPSON e = new();
    public static List<byte[]> GetBillPrintInstructions(PdfRequest request, int lineCharacters)
    {
        List<byte[]> printInstructions = [
            ..Header(request),
            ..Body(request, lineCharacters),
            ..Footer(request, lineCharacters)
        ];
        return printInstructions;
    }

    private static List<byte[]> Body(PdfRequest request, int lineCharacters)
    {
        List<byte[]> body = [
           e.SetStyles(PrintStyle.FontB),
            e.FeedLines(3),
            e.LeftAlign(),
         ];

        foreach (var item in request.Items)
        {
            string left = $"{item.Count,-2} {item.Name}";
            body.Add(PrintColumnLine(left, $"{item.Price:0.00}", $"{item.TotalPrice:0.00}", lineCharacters));
            foreach (var subItem in item.Items ?? [])
            {
                if (subItem.Price > 0)
                {
                    string subItemLeft = $"   {subItem.Name}";
                    body.Add(PrintColumnLine(subItemLeft, $"{subItem.Price:0.00}", " ", lineCharacters));
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

    private static List<byte[]> Footer(PdfRequest request, int lineCharacters)
    {
        int boldLineChars = lineCharacters - (lineCharacters / 4);

        List<byte[]> divisions = [];
        foreach (var division in request.Divisions.Where(x => x.FriendlyName != null))
        {
            divisions.Add(PrintColumnLine(division.FriendlyName!, $"{division.Total:0.00}", lineCharacters));
        }

        byte[] tip = request.IsClosed ? PrintColumnLine("Tip", $"{request.TipAmount:0.00}", boldLineChars) : [];

        List<byte[]> footer = [
            PrintCharLine('-', lineCharacters),
            ..divisions,
            e.SetStyles(PrintStyle.Bold),
            PrintColumnLine("Total", $"{request.Total:0.00}", boldLineChars),
            e.SetStyles(PrintStyle.None),
            PrintColumnLine("Total Excluding VAT", $"{request.TotalExVAT:0.00}", boldLineChars),
            PrintColumnLine("VAT", $"{request.VAT:0.00}", boldLineChars),
            PrintColumnLine("Payment Received", $"{request.PaymentReceived:0.00}", boldLineChars),
            tip,
            e.SetStyles(PrintStyle.FontB),
            PrintCharLine('-', lineCharacters),
            e.PrintLine(""),
            PrintColumnLine("Tip: ", "______________________________", lineCharacters),
            e.PrintLine(""),
            PrintColumnLine("Total: ", "______________________________", lineCharacters),
            e.FeedLines(5),
            e.FullCut()
        ];
        return footer;
    }

    private static byte[] PrintCharLine(char c, int lineCharacters)
    {
        string result = new(c, lineCharacters);
        return e.PrintLine(result);
    }

    private static byte[] PrintColumnLine(string left, string right, int maxLength = 64)
    {
        // Ensure left and right are not null
        string fLeft = left ?? string.Empty;
        string fRight = right ?? string.Empty;
        const string ELLIPSIS = "...";
        const int MIN_SPACE = 1;

        if (fRight.Length > maxLength)
        {
            fLeft = string.Empty;
            fRight = fRight.Substring(0, maxLength);
        }

        // Calculate totalLength based on potentially modified right
        int totalLength = fLeft.Length + fRight.Length + MIN_SPACE;

        if (totalLength > maxLength) // This condition implies left needs truncation
        {
            // Space available for left, considering "..." and MIN_SPACE
            int availableForLeftContent = maxLength - fRight.Length - ELLIPSIS.Length - MIN_SPACE;
            if (availableForLeftContent < 0)
            {
                fLeft = string.Empty; // No space for left text, not even "..."
            }
            else if (fLeft.Length > (maxLength - fRight.Length - MIN_SPACE))
            {
                fLeft = fLeft.Substring(0, Math.Max(0, availableForLeftContent)) + ELLIPSIS;
            }
            // If fLeft.Length was already short enough to fit with "..." or as is, no change to fLeft here.
        }

        int currentTextLength = fLeft.Length + fRight.Length;
        int spaces = Math.Max(MIN_SPACE, maxLength - currentTextLength); // Ensure at least MIN_SPACE

        string line = fLeft.PadRight(fLeft.Length + spaces) + fRight;
        return e.PrintLine(line);
    }

    private static byte[] PrintColumnLine(string left, string center, string right, int maxLength)
    {
        string fL = left ?? string.Empty;
        string fC = center ?? string.Empty;
        string fR = right ?? string.Empty;
        const string ELLIPSIS = "...";
        const int MIN_SPACE = 1;

        // If right is too long, always show the end of it
        if (fR.Length > maxLength)
        {
            fR = fR.Substring(fR.Length - maxLength, maxLength);
            fL = string.Empty;
            fC = string.Empty;
        }

        // Define a fixed start position for center text
        int centerStart = maxLength - 13;

        // Calculate available space for left text (before center)
        int availableForL = centerStart - MIN_SPACE;
        if (availableForL < 0) availableForL = 0;

        // Truncate left if needed (with ellipsis)
        if (fL.Length > availableForL)
        {
            int ellipsisLen = ELLIPSIS.Length;
            int take = Math.Max(0, availableForL - ellipsisLen);
            fL = fL[..take] + ELLIPSIS;
        }

        // Calculate available space for center text
        int availableForC = maxLength - centerStart - fR.Length;
        if (availableForC < 0) availableForC = 0;

        // Truncate center if needed
        if (fC.Length > availableForC)
        {
            int ellipsisLen = ELLIPSIS.Length;
            int take = Math.Max(0, availableForC - ellipsisLen);
            fC = fC[..take] + ELLIPSIS;
        }

        // Compose the line
        // Left part
        string leftPart = fL.PadRight(centerStart);

        // Center part (always starts at centerStart)
        string centerPart = fC;

        // Calculate spaces between center and right
        int used = leftPart.Length + centerPart.Length + fR.Length;
        int spaces = Math.Max(MIN_SPACE, maxLength - used);

        string line = leftPart + centerPart + new string(' ', spaces) + fR;
        // If line is too long, trim it
        if (line.Length > maxLength)
            line = line[..maxLength];

        return e.PrintLine(line);
    }
}