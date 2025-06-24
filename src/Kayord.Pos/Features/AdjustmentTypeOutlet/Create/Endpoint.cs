using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;


namespace Kayord.Pos.Features.AdjutmentTypeOutlet.Create;

public class Endpoint : Endpoint<Request, Pos.Entities.Menu>
{
    private readonly AppDbContext _dbContext;
    private readonly RedisClient _redisClient;
    private readonly UserService _userService;


    public Endpoint(AppDbContext dbContext, RedisClient redisClient, UserService userService)
    {
        _dbContext = dbContext;
        _redisClient = redisClient;
        _userService = userService;
    }

    public override void Configure()
    {
        Post("/adjustmentType");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (!await _userService.IsManager(req.OutletId))
        {
            await SendForbiddenAsync();
            return;
        }

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
        CashUpUserItemType cashUpUserItemType = new()
        {
            AdjustmentTypeId = adjustmentTypeEntity.AdjustmentTypeId,
            CashUpUserItemRule = Common.Enums.CashUpUserItemRule.Adjustment,
            AffectsGrossBalance = false,
            Position = 99,
            IsAuto = true,
            ItemType = adjustmentTypeEntity.Name
        };

        await _dbContext.CashUpUserItemType.AddAsync(cashUpUserItemType);
        await _dbContext.SaveChangesAsync();

        await Helper.ClearCacheOutlet(_dbContext, _redisClient, req.OutletId);
    }
}
