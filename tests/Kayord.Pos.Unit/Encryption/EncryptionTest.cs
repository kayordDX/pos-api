using Kayord.Pos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kayord.Pos.Unit.Encryption;

public class Tests(App app) : TestBase<App>
{
    [Fact]
    public void Sample_Test()
    {
        // app.CreateClient()
        (1 + 1).Should().Be(2);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("longer string with spaces")]
    [InlineData("&%$#!$%$!@#")]
    [InlineData("00000000000")]
    public void Encrypt_Test(string plainText)
    {
        var encryptionService = app.Services.GetRequiredService<EncryptionService>();
        encryptionService.Should().NotBeNull();
        if (encryptionService == null)
        {
            return;
        }
        var iv = encryptionService.GenerateIV();
        iv.Should().NotBeNull();

        var encryptedValue = encryptionService.Encrypt(plainText, iv);
        var decryptedValue = encryptionService.Decrypt(encryptedValue, iv);
        decryptedValue.Should().Be(plainText);
    }
}