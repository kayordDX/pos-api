using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Order.Get;

public class Endpoint : Endpoint<Request, StockOrderDTO>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/stock/order/{Id}");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.StockOrder
            .Where(x => x.Id == req.Id)
            .ProjectToDto()
            .FirstOrDefaultAsync(ct);

        if (results == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        await Send.OkAsync(results);
    }
}



