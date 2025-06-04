using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.MenuItem.Delete;

public class Endpoint : Endpoint<Request, Pos.Entities.MenuSection>
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
        Delete("/menuItem/{id}");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.MenuItem? menuItem = await _dbContext.MenuItem
        .Include(x => x.MenuSection)
        .ThenInclude(x => x.Menu)
        .FirstOrDefaultAsync(x => x.MenuItemId == req.Id);

        if (menuItem != null)
        {
            _dbContext.MenuItem.Remove(menuItem);
            await _dbContext.SaveChangesAsync();
            await Helper.ClearCacheOutlet(_dbContext, _redisClient, menuItem.MenuSection.Menu!.OutletId);
        }

        else
        {
            throw new Exception("Sorry, the princess is in another castle.");
        }
    }

}


