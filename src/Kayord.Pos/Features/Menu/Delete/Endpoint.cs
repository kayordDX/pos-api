using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Menu.Delete;

public class Endpoint : Endpoint<Request>
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
        Delete("/menu/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.Menu.FirstOrDefaultAsync(x => x.Id == req.Id);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        Entities.MenuSection? menuSection = await _dbContext.MenuSection.FirstOrDefaultAsync(x => x.MenuId == req.Id);
        if (menuSection == null)
        {
            _dbContext.Menu.Remove(entity);
            await _dbContext.SaveChangesAsync();
            await Helper.ClearCacheOutlet(_dbContext, _redisClient, entity.OutletId);
            await Send.NoContentAsync();
        }
        else
        {
            throw new Exception("Can't Delete Menu containing Menu Sections");
        }
    }
}