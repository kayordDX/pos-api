using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.MenuItem.Update;

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
        Put("/menuItem");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.MenuItem? menuItem = await _dbContext.MenuItem.FindAsync(req.Id);

        if (menuItem != null)
        {
            menuItem.MenuSectionId = req.MenuSectionId;
            menuItem.Name = req.Name;
            menuItem.Description = req.Description;
            menuItem.Price = req.Price;
            menuItem.Position = req.PositionId;
            menuItem.DivisionId = req.DivisionId;
            menuItem.IsAvailable = req.IsAvailable;
            menuItem.IsEnabled = req.IsEnabled;
            menuItem.StockPrice = req.StockPrice;

            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Menu Section not found");
        }


    }
}
