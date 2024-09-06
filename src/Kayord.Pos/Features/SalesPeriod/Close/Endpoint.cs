using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.SalesPeriod.Close;

public class Endpoint : Endpoint<Request, Entities.SalesPeriod>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/salesPeriod/close");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.SalesPeriod.FindAsync(req.SalesPeriodId);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }
        var OpenTableCount = await _dbContext.TableBooking.Where(x => x.SalesPeriodId == req.SalesPeriodId && x.CloseDate == null).CountAsync();
        if (OpenTableCount > 0)
        {
            throw new Exception("Cannot close sales period with open tables");
        }
        entity.EndDate = DateTime.Now;

        // Clock out all users
        _dbContext.Clock
            .Where(x => x.OutletId == entity.OutletId && x.EndDate == null)
            .ToList()
            .ForEach(x => x.EndDate = DateTime.Now);

        await _dbContext.SaveChangesAsync();
        await SendAsync(entity);
    }
}