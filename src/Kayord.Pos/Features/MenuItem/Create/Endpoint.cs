using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.MenuItem.Create;

public class Endpoint : Endpoint<Request, Pos.Entities.MenuItem>
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
        Post("/menuItem");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.MenuSection? menuSection = await _dbContext.MenuSection.FirstOrDefaultAsync(x => x.MenuSectionId == req.MenuSectionId);

        if (menuSection != null)
        {

            Entities.MenuItem menuItem = new()
            {
                MenuSectionId = req.MenuSectionId,
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
                Position = req.PositionId,
                DivisionId = req.DivisionId,
                IsAvailable = req.IsAvailable,
                IsEnabled = req.IsEnabled,
                StockPrice = req.StockPrice
            };
            await _dbContext.MenuItem.AddAsync(menuItem);
            await _dbContext.SaveChangesAsync();

            if (req.ExtraGroupIds != null)
            {

                var receivedExtraGroupIds = req.ExtraGroupIds.ToHashSet();

                var newExtraGroups = receivedExtraGroupIds.Select(id => new Entities.MenuItemExtraGroup
                {
                    ExtraGroupId = id,
                    MenuItemId = menuItem.MenuItemId
                });
                await _dbContext.MenuItemExtraGroup.AddRangeAsync(newExtraGroups);

            }
            await _dbContext.SaveChangesAsync();

            Entities.Menu? menu = await _dbContext.Menu.FindAsync(menuSection.MenuId);
            if (menu != null)
                await Helper.ClearCacheOutlet(_dbContext, _redisClient, menu.OutletId);

        }
        else
        {
            throw new Exception("Menu Section not found");
        }


    }
}
