using Kayord.Pos.Data;
using Kayord.Pos.Data.Migrations;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.AssignOutlet;

public class Endpoint : Endpoint<Request, Entities.UserOutlet>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Post("/user/assignOutlet");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        if (_cu.UserId == null)
        {
            await SendForbiddenAsync();
            return;
        }

        var outlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.OutletId == req.OutletId);
        if (outlet == null)
        {
            outlet = new()
            {
                OutletId = req.OutletId,
                UserId = _cu.UserId,
                isCurrent = true
            };
            await _dbContext.AddAsync(outlet);
        }
        else
        {
            outlet.isCurrent = true;
        }
        await _dbContext.SaveChangesAsync();
        await SendAsync(outlet);
    }
}
