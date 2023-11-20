
using System.Security.Claims;

namespace Kayord.Pos.Features.Token;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly IConfiguration _config;

    public Endpoint(IConfiguration config)
    {
        _config = config;
    }

    public override void Configure()
    {
        Post("/token");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var roles = new List<string> { };
        var permissions = new List<string> { };
        var claims = new List<Claim> {
            new Claim("name", "Steff Bosch"),
            new Claim("email", "boshkoppie@gmail.com"),
            new Claim("type", "Waiter")
        };
        var expiresAt = DateTime.Now.AddMinutes(1);
        await SendAsync(new Response
        {
            Token = Common.Security.Token.CreateToken(_config.GetValue<string>("SigningKey") ?? string.Empty, expiresAt, permissions, roles, claims)
        });
    }
}