using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Clock.ClockOut;

public class Endpoint : Endpoint<Request, Pos.Entities.Clock>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/clockout");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Clock.FirstOrDefaultAsync(x => x.StaffId == req.StaffId && x.OutletId == req.OutletId && x.EndDate == null);
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
