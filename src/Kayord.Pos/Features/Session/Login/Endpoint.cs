
using System.Security.Claims;
using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Session.Login;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _dbContext;

    public Endpoint(IConfiguration config, AppDbContext dbContext)
    {
        _config = config;
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/session/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Staff.FindAsync(req.StaffId);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }
        var roles = new List<string> { };
        var permissions = new List<string> { };
        int typeId = (int)entity.StaffType;
        var claims = new List<Claim> {
            new Claim("id", entity.Id.ToString()),
            new Claim("name", entity.Name),
            new Claim("type", typeId.ToString())
        };
        var expiresAt = DateTime.Now.AddHours(3);
        await SendAsync(new Response
        {
            Token = Common.Security.Token.CreateToken(_config.GetValue<string>("SigningKey") ?? string.Empty, expiresAt, permissions, roles, claims)
        });
    }
}