using System.Security.Cryptography;
using System.Text;

namespace Kayord.Pos.Common;

public static class Utils
{
    private static readonly char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
    public static string GenerateOTP()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var bytes = new byte[8];
            rng.GetBytes(bytes);

            var result = new StringBuilder(8);
            foreach (var byteValue in bytes)
            {
                result.Append(chars[byteValue % chars.Length]);
            }

            return result.ToString();
        }
    }

}