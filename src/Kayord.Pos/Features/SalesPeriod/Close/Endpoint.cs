using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.SalesPeriod.Close;

public class Endpoint : Endpoint<Request, Pos.Entities.SalesPeriod>
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
            await SendForbiddenAsync();
            return;
        }
        entity.EndDate = DateTime.Now;
        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.SalesPeriod.FindAsync(entity.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(result);
    }
}