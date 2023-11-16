using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Kayord.Pos.Common.Security;

public static class Token
{
    public static string CreateToken(
        string signingKey,
        DateTime? expireAt = null,
        IEnumerable<string>? permissions = null,
        IEnumerable<string>? roles = null,
        IEnumerable<Claim>? claims = null,
        string? issuer = null,
        string? audience = null
    )
    {
        var claimList = new List<Claim>();

        if (claims != null)
            claimList.AddRange(claims);

        if (permissions != null)
            claimList.AddRange(permissions.Select(p => new Claim("permissions", p)));

        if (roles != null)
            claimList.AddRange(roles.Select(r => new Claim("role", r)));

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            IssuedAt = DateTime.UtcNow,
            Subject = new(claimList),
            Expires = expireAt,
            SigningCredentials = GetSigningCredentials(signingKey)
        };

#if NET8_0_OR_GREATER
        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(descriptor);
#else
        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(handler.CreateToken(descriptor));
#endif
    }

    static SigningCredentials GetSigningCredentials(string key)
    {
        return new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature);
        // var rsa = RSA.Create(); // don't dispose this
        // rsa.ImportRSAPrivateKey(Convert.FromBase64String(key), out _);

        // return new(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
    }
}