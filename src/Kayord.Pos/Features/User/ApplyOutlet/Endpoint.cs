using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.ApplyOutlet;

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
        Post("/user/applyOutlet");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (_cu.UserId == null)
        {
            await Send.ForbiddenAsync();
            return;
        }
        var hasApplied = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.OutletId == req.OutletId);
        if (hasApplied != null)
        {
            throw new Exception("Already applied for this outlet");
        }
        else
        {
            await _dbContext.UserOutlet.AddAsync(new()
            {
                UserId = _cu.UserId,
                OutletId = req.OutletId
            });
        }

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
