using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Menu.Sections.Create;

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
        Post("/menuSection");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.Menu? menu = await _dbContext.Menu.FirstOrDefaultAsync(x => x.Id == req.MenuId);

        if (menu != null)
        {
            var menuSection = new Entities.MenuSection
            {
                MenuId = req.MenuId,
                Name = req.Name,
                PositionId = req.PositionId,

            };

            await _dbContext.MenuSection.AddAsync(menuSection);
            await _dbContext.SaveChangesAsync();
            await Helper.ClearCacheOutlet(_dbContext, _redisClient, menu.OutletId);

        }
        else
        {
            throw new Exception("Menu not found");
        }


    }
}
