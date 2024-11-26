using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Extra.Create;

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
        Post("/extra");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.ExtraGroup? extraGroup = await _dbContext.ExtraGroup.FirstOrDefaultAsync(x => x.ExtraGroupId == req.ExtraGroupId);

        if (extraGroup == null)
        {
            throw new Exception("Extra Group not found");
        }

        Entities.Extra extra = new()
        {
            Name = req.Name,
            PositionId = req.PositionId,
            Price = req.Price,
            ExtraGroupId = req.ExtraGroupId,
            OutletId = req.OutletId
        };

        await _dbContext.Extra.AddAsync(extra);
        await _dbContext.SaveChangesAsync();

        // await Helper.ClearCacheOutlet(_dbContext, _redisClient, req.OutletId);
        await SendNoContentAsync();
    }
}
