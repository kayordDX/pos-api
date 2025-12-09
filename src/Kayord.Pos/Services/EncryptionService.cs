using System.Security.Cryptography;
using System.Text;
using Kayord.Pos.Config;
using Microsoft.Extensions.Options;

namespace Kayord.Pos.Services;

public class EncryptionService
{
    private readonly AppConfig _appConfig;
    private readonly byte[] _key;
    public EncryptionService(IOptions<AppConfig> appConfig)
    {
        _appConfig = appConfig.Value;
        _key = CreateKeyFromString();
    }

    private byte[] CreateKeyFromString()
    {
        byte[] salt = Encoding.UTF8.GetBytes(_appConfig.EncryptionSalt);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(_appConfig.EncryptionKey);
        return Rfc2898DeriveBytes.Pbkdf2(
            passwordBytes,
            salt,
            10000,
            HashAlgorithmName.SHA512,
            16
        );
    }

    public string Encrypt(string plainText, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        {
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(plainText);
            }
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string cipherText, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }

    public byte[] GenerateIV()
    {
        using var aes = Aes.Create();
        aes.GenerateIV();
        return aes.IV;
    }
}