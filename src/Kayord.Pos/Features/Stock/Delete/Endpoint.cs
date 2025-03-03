using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/stock/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Stock.FirstOrDefaultAsync(x => x.Id == req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }
        _dbContext.Stock.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}