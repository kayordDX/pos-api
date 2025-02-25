using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Stock.Items.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/stock/items");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockItem.FindAsync(req.Id);
        if (entity == null)
        {
            entity = new Entities.StockItem()
            {
                DivisionId = req.DivisionId,
                StockId = req.StockId,
                Actual = req.Actual,
                Threshold = req.Threshold
            };
            await _dbContext.AddAsync(entity);
        }

        entity.Actual = req.Actual;
        entity.Threshold = req.Threshold;

        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}
