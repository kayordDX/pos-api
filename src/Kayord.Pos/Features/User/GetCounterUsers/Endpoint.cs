using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.GetCounterUsers;

public class Endpoint : Endpoint<Request, List<Response>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/user/counter");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var users = await _dbContext.UserOutletPin
            .Where(x => x.OutletId == req.OutletId)
            .Select(x => new Response() { UserId = x.UserId, Image = x.User.Image, Name = x.User.Name })
            .ToListAsync(ct);

        await Send.OkAsync(users);
    }
}