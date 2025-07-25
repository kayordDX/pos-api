using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Clock.ClockIn;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _user;

    public Endpoint(AppDbContext dbContext, CurrentUserService user)
    {
        _dbContext = dbContext;
        _user = user;
    }

    public override void Configure()
    {
        Post("/clock/in");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (_user.UserId == null)
        {
            await Send.ForbiddenAsync();
            return;
        }

        var exists = await _dbContext.Clock.FirstOrDefaultAsync(x => x.UserId == _user.UserId && x.OutletId == req.OutletId && x.EndDate == null);
        if (exists != null)
        {
            await Send.ForbiddenAsync();
            return;
        }
        else
        {
            Entities.Clock entity = new Entities.Clock()
            {
                UserId = _user.UserId,
                StartDate = DateTime.Now,
                EndDate = null,
                OutletId = req.OutletId
            };
            await _dbContext.Clock.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            await Send.NoContentAsync();
        }

    }

}
