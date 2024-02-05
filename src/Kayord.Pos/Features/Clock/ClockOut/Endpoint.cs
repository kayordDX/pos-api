using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Clock.ClockOut;

public class Endpoint : Endpoint<Request, Pos.Entities.Clock>
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
        Post("/clock/out");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Clock.FirstOrDefaultAsync(x => x.UserId == _user.UserId && x.OutletId == req.OutletId && x.EndDate == null);
        if (entity == null)
        {
            await SendForbiddenAsync();
            return;
        }
        else
        {
            entity.EndDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Clock.FindAsync(entity.Id);
            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }
            await SendAsync(result);
        }

    }

}
