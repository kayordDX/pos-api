using Kayord.Pos.Config;
using Kayord.Pos.Services;
using Microsoft.Extensions.Options;

namespace Unit.Encryption;

public class EncryptionTest
{
    private readonly EncryptionService _encryptionService;
    public EncryptionTest()
    {
        var options = Options.Create(new AppConfig
        {
            EncryptionKey = "Your16CharKeyHere",
            EncryptionSalt = "Your16CharSaltHere",
        });
        _encryptionService = new EncryptionService(options);
    }

    [Fact]
    public void Sample_Test()
    {
        Assert.Equivalent(1 + 1, 2);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("longer string with spaces")]
    [InlineData("&%$#!$%$!@#")]
    [InlineData("00000000000")]
    public void Encrypt_Test(string plainText)
    {
        var iv = _encryptionService.GenerateIV();
        Assert.NotNull(iv);

        var encryptedValue = _encryptionService.Encrypt(plainText, iv);
        var decryptedValue = _encryptionService.Decrypt(encryptedValue, iv);
        Assert.Equivalent(decryptedValue, plainText);
    }

    [Fact]
    public void Encrypt_To()
    {
        string plainText = "test";
        // Create static byte array for IV generation
        var iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        // var iv = encryptionService.GenerateIV();
        Assert.NotNull(iv);

        var encryptedValue = _encryptionService.Encrypt(plainText, iv);
        // var decryptedValue = encryptionService.Decrypt(encryptedValue, iv);
        Assert.Equivalent(encryptedValue, "+V4m2QVQZe4qEI7H3e72Lw==");

        var decryptedValue = _encryptionService.Decrypt(encryptedValue, iv);
        Assert.Equivalent(decryptedValue, plainText);
    }

    [Fact]
    public void Encrypt_Long()
    {
        string plainText = "KzI3ODQyNTAyMzExLmVhYzNhNTg3LTQ4NTUtNGY5YS1hNmMxLTNkMDA1NjM3OWVlYg==";
        // Create static byte array for IV generation
        var iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        // var iv = encryptionService.GenerateIV();
        Assert.NotNull(iv);

        var encryptedValue = _encryptionService.Encrypt(plainText, iv);
        // var decryptedValue = encryptionService.Decrypt(encryptedValue, iv);
        Assert.Equivalent(encryptedValue, "rwVu347RW9zuRZjC1Tim+k3VJCtjGoOANQuKtqYrnu+xKzp+eqt+Sq/U+9vg2P1o4b3DQlOesoywGS/e4F6B6ubdqYfeYY6XxAibANtWS94=");

        var decryptedValue = _encryptionService.Decrypt(encryptedValue, iv);
        Assert.Equivalent(decryptedValue, plainText);
    }
}