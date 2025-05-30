using Kayord.Pos.Data;
using Kayord.Pos.Services;


namespace Kayord.Pos.Features.AdjutmentTypeOutlet.Create;

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
        Post("/adjustmentType");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var adjustmentTypeEntity = new Entities.AdjustmentType
        {
            Name = req.Name,
            Description = req.Description
        };

        await _dbContext.AdjustmentType.AddAsync(adjustmentTypeEntity);

        await _dbContext.SaveChangesAsync();
        var adjustmentTypeOutlet = new Entities.AdjustmentTypeOutlet
        {
            OutletId = req.OutletId,
            AdjustmentTypeId = adjustmentTypeEntity.AdjustmentTypeId
        };
        await _dbContext.AdjustmentTypeOutlet.AddAsync(adjustmentTypeOutlet);

        await _dbContext.SaveChangesAsync();

        await Helper.ClearCacheOutlet(_dbContext, _redisClient, req.OutletId);
    }
}
