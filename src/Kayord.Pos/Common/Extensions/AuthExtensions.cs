using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Kayord.Pos.Common.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.Authority = configuration["Auth:ValidIssuer"];
                o.Audience = configuration["Auth:Audience"];
                o.TokenValidationParameters.ValidIssuer = configuration["Auth:ValidIssuer"];
            });
        // o =>
        // {
        //     SecurityKey key;

        //     key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSigningKey));

        //     // var rsa = RSA.Create();
        //     // rsa.ImportRSAPublicKey(Convert.FromBase64String(tokenSigningKey), out _);
        //     // key = new RsaSecurityKey(rsa);

        //     o.TokenValidationParameters.IssuerSigningKey = key;
        //     o.TokenValidationParameters.ValidateIssuerSigningKey = true;
        //     o.TokenValidationParameters.ValidateLifetime = true;
        //     o.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(60);
        //     o.TokenValidationParameters.ValidAudience = null;
        //     o.TokenValidationParameters.ValidateAudience = false;
        //     o.TokenValidationParameters.ValidIssuer = null;
        //     o.TokenValidationParameters.ValidateIssuer = false;

        //     jwtOptions?.Invoke(o);
        // });

        services.AddAuthorization();
        return services;
    }
}