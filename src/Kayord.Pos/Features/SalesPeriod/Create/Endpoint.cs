using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.SalesPeriod.Create;

public class Endpoint : Endpoint<Request, Pos.Entities.SalesPeriod>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/salesPeriod");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var exists = await _dbContext.SalesPeriod
            .Where(x => x.EndDate == null && x.OutletId == req.OutletId)
            .FirstOrDefaultAsync(ct);

        if (exists != null)
        {
            throw new Exception("Sales Period already exists");
        }

        Entities.SalesPeriod entity = new Entities.SalesPeriod()
        {
            Name = req.Name,
            OutletId = req.OutletId,
            StartDate = DateTime.Now
        };
        await _dbContext.SalesPeriod.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.SalesPeriod.FindAsync(entity.Id);
        if (result == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        await Send.OkAsync(result);
    }
}