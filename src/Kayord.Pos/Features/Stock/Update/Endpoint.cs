using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Stock.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/stock");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Stock.FindAsync(req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        entity.Name = req.Name;
        entity.UnitId = req.UnitId;
        entity.HasVat = req.HasVat;
        entity.StockCategoryId = req.StockCategoryId;

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
