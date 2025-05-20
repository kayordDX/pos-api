using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Stock.Create;

public class Endpoint : Endpoint<Request, Entities.StockOrder>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/stock");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new Entities.Stock
        {
            OutletId = req.OutletId,
            Name = req.Name,
            UnitId = req.UnitId,
            StockCategoryId = req.StockCategoryId,
            HasVat = req.HasVat,
        };

        await _dbContext.Stock.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
