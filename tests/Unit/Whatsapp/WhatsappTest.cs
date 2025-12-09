using Kayord.Pos.Services.Whatsapp;
using Xunit;

namespace Unit.Whatsapp;

public class WhatsappTest
{
    [Theory]
    [InlineData("0123456789", null, "27123456789")]
    [InlineData("+123456789", null, "27123456789")]
    [InlineData("00123456789", null, "27123456789")]
    [InlineData("123456789", null, "27123456789")]
    [InlineData("0123456789", "91", "91123456789")]
    [InlineData("+0123456789", "91", "91123456789")]
    [InlineData("+00123456789", "91", "91123456789")]
    [InlineData("0", null, "27")]
    [InlineData("+0", null, "27")]
    [InlineData("", null, "27")]
    public void GetNumberWithCountryCode_StripsLeadingZeroOrPlus(string number, string? countryCode, string expected)
    {
        // Arrange
        var service = new WhatsappService(default!, default!);

        // Act
        var result = service.GetNumberWithCountryCode(number, countryCode);

        // Assert
        Assert.Equal(expected, result);
    }
}