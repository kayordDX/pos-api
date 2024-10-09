using Kayord.Pos.Data;
using Kayord.Pos.Services;

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
        Entities.UserOutlet outlet = new();
        if (_cu.UserId == null)
        {
            await SendForbiddenAsync();
            return;
        }
        var UserOutlets = _dbContext.UserOutlet.Where(x => x.UserId == _cu.UserId);
        bool hasCurrentOutlet = false;
        foreach (Entities.UserOutlet uo in UserOutlets)
        {
            if (uo.OutletId == req.OutletId)
            {
                uo.IsCurrent = true;
                hasCurrentOutlet = true;
                outlet = uo;
            }
            else
            {
                uo.IsCurrent = false;
            }
        }

        if (!hasCurrentOutlet)
        {
            outlet = new()
            {
                OutletId = req.OutletId,
                UserId = _cu.UserId,
                IsCurrent = true
            };
            await _dbContext.AddAsync(outlet);
        }

        await _dbContext.SaveChangesAsync();
        await SendAsync(outlet);
    }
}
