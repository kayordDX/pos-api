using Kayord.Pos.Common.Printer.Emitters;

namespace Unit.Print;

public class PrintTest
{
    private static readonly EPSON e = new();

    [Fact]
    public void PrintBasic()
    {
        string data = "Hello World";
        var print1 = e.PrintLine(data);

        byte[] print2 = [72, 101, 108, 108, 111, 32, 87, 111, 114, 108, 100, 10];
        Assert.Equal(print1, print2);
    }
}