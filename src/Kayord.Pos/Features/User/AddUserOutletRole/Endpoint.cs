using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.AddUserOutletRole;

public class Endpoint : Endpoint<Request>
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
        Post("/user/role");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken c)
    {
        var userOutlet = await _dbContext.UserOutlet.Where(x => x.UserId == _cu.UserId && x.IsCurrent == true).FirstOrDefaultAsync(c);
        if (userOutlet == null)
        {
            throw new Exception("Could not find outlet for user");
        }

        var roleExists = await _dbContext.UserRoleOutlet
            .Where(x => x.UserId == req.UserId && x.RoleId == req.RoleId && x.OutletId == userOutlet.OutletId)
            .FirstOrDefaultAsync(c);
        if (roleExists != null)
        {
            throw new Exception("Role already exists");
        }

        var roleEntity = new Entities.UserRoleOutlet
        {
            RoleId = req.RoleId,
            UserId = req.UserId,
            OutletId = userOutlet.OutletId
        };

        _dbContext.UserRoleOutlet.Add(roleEntity);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}