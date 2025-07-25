using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Supplier.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/supplier/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Supplier.FirstOrDefaultAsync(x => x.Id == req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        _dbContext.Supplier.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}