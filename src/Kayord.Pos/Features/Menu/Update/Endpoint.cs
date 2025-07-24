using Kayord.Pos.Data;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Menu.Update;

public class Endpoint : Endpoint<Request, Pos.Entities.Menu>
{
    private readonly AppDbContext _dbContext;
    private readonly RedisClient _redisClient;

    public Endpoint(AppDbContext dbContext, RedisClient redisClient)
    {
        _dbContext = dbContext;
        _redisClient = redisClient;
    }

    public override void Configure()
    {
        Put("/menu");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Menu.FindAsync(req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        entity.Name = req.Name;
        entity.Position = req.Position;

        await _dbContext.SaveChangesAsync();
        await Helper.ClearCacheOutlet(_dbContext, _redisClient, entity.OutletId);
        await SendAsync(entity);
    }
}