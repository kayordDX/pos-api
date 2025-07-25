using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay.PayConfig.Delete;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Delete("/pay/config/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        HaloConfig? entity = await _dbContext.HaloConfig.FirstOrDefaultAsync(x => x.Id == req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        if (entity.IsEnabled == true)
        {
            throw new Exception("Cannot delete active config");
        }

        _dbContext.HaloConfig.Remove(entity);
        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
