using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Printer.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/printer/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Printer? entity = await _dbContext.Printer.FirstOrDefaultAsync(x => x.Id == req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        _dbContext.Printer.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
