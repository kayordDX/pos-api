using Kayord.Pos.Data;

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
        Post("/salesperiod");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Pos.Entities.SalesPeriod entity = new Pos.Entities.SalesPeriod()
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
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(result);
    }
}