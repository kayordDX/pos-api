using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Menu.Sections.Delete;

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
        Delete("/menuSection/{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.MenuSection? menuSection = await _dbContext.MenuSection.FirstOrDefaultAsync(x => x.MenuSectionId == req.Id);

        if (menuSection != null)
        {
            Entities.MenuItem? menuItem = await _dbContext.MenuItem.FirstOrDefaultAsync(x => x.MenuSectionId == req.Id);
            // Do not delete menu section if it contains menu items
            if (menuItem == null)
            {
                Entities.Menu? menu = await _dbContext.Menu.FirstOrDefaultAsync(x => x.Id == req.Id);
                if (menu != null)
                {
                    _dbContext.MenuSection.Remove(menuSection);
                    await _dbContext.SaveChangesAsync();
                    await Helper.ClearCacheOutlet(_dbContext, _redisClient, menu!.OutletId);
                }
            }
            else
            {
                throw new Exception("Can't Delete Section containing Menu Items");
            }
        }
        else
        {
            throw new Exception("Menu Section not found");
        }


    }
}
