
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
        await SendAsync(new Response
        {
            Token = Common.Security.Token.CreateToken(_config.GetValue<string>("SigningKey") ?? string.Empty)
        });
    }
}