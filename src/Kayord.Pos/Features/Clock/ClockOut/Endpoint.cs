using Kayord.Pos.Data;
using Kayord.Pos.Entities;

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
        var exists = _dbContext.Clock.FirstOrDefault(x=>x.StaffId == req.StaffId && x.SalesPeriodId == req.SalesPeriodId && x.EndDate != null);
        if(exists != null)
        {
            await SendForbiddenAsync();
            return;
        }
        else{
        var entity = _dbContext.Clock.FirstOrDefault(x=>x.StaffId == req.StaffId && x.SalesPeriodId == req.SalesPeriodId && x.EndDate == null);
        if(entity == null)
        {
             await SendNotFoundAsync(); 
             return;
        }
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
