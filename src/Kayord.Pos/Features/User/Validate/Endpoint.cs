using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.Validate;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly AppDbContext _dbContext;


    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/user/validate");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == req.UserId);
        if (user == null)
        {
            await _dbContext.User.AddAsync(new Entities.User
            {
                Email = req.Email,
                UserId = req.UserId,
                Image = req.Image ?? "",
                Name = req.Name,
                IsActive = true
            });
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            user.Email = req.Email;
            user.Image = req.Image ?? "";
            user.Name = req.Name;
            await _dbContext.SaveChangesAsync();
        }

        Response r = new()
        {
            UserId = req.UserId ?? ""
        };
        await SendAsync(r);
    }
}
