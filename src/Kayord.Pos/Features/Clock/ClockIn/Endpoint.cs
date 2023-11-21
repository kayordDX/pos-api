using Kayord.Pos.Data;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.Clock.ClockIn;

public class Endpoint : Endpoint<Request, Pos.Entities.Clock>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/clockin");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var exists = _dbContext.Clock.FirstOrDefault(x=>x.StaffId == req.StaffId && x.SalesPeriodId == req.SalesPeriodId );
        if(exists != null && exists.EndDate == null)
        {
            await SendForbiddenAsync();
            return;
        }
        else{
        Pos.Entities.Clock entity = new Pos.Entities.Clock()
        {
            StaffId = req.StaffId,
            StartDate = DateTime.Now,
            EndDate = null,
            SalesPeriodId = req.SalesPeriodId
        };
        await _dbContext.Clock.AddAsync(entity);
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
