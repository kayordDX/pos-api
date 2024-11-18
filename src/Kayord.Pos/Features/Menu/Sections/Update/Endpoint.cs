using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Menu.Sections.Update;

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
        Put("/menuSection");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.MenuSection? menuSection = await _dbContext.MenuSection.FindAsync(req.Id);

        if (menuSection != null)
        {
            menuSection.Name = req.Name;
            menuSection.PositionId = req.PositionId;
            // menuSection.MenuId = req.MenuId;

            await _dbContext.SaveChangesAsync();

            Entities.Menu? menu = await _dbContext.Menu.FirstOrDefaultAsync(x => x.Id == menuSection.MenuId);
            if (menu != null)
            {
                await Helper.ClearCacheOutlet(_dbContext, _redisClient, menu.OutletId);
            }
        }
        else
        {
            throw new Exception("Menu section not found");
        }


    }
}
