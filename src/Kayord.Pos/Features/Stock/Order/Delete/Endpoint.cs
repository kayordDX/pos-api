using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Order.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/stock/order/{id}");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockOrder.FirstOrDefaultAsync(x => x.Id == req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }
        _dbContext.StockOrder.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}