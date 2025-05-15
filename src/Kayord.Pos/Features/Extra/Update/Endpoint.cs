using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Extra.Update;

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
        Put("/extra");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.Extra? extraEntity = await _dbContext.Extra.FindAsync(req.ExtraId);
        if (extraEntity == null)
        {
            throw new Exception("Sorry, the princess is in another castle.");
        }

        if (req.ExtraGroupId != extraEntity.ExtraGroupId)
        {
            Entities.ExtraGroup? extraGroup = await _dbContext.ExtraGroup.FirstOrDefaultAsync(x => x.ExtraGroupId == req.ExtraGroupId);
            if (extraGroup == null)
            {
                throw new Exception("Extra Group not found");
            }
        }

        extraEntity.Name = req.Name;
        extraEntity.Price = req.Price;
        extraEntity.PositionId = req.PositionId;
        extraEntity.ExtraGroupId = req.ExtraGroupId;
        extraEntity.OutletId = req.OutletId;
        extraEntity.IsAvailable = req.IsAvailable;

        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
        // await Helper.ClearCacheOutlet(_dbContext, _redisClient, req.OutletId);

    }


}

