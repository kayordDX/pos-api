using Kayord.Pos.Data;
using Kayord.Pos.Data.Migrations;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Extra.GroupUpdate;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;
    private readonly RedisClient _redisClient;
    private readonly CurrentUserService _user;

    public Endpoint(AppDbContext dbContext, RedisClient redisClient, CurrentUserService user)
    {
        _dbContext = dbContext;
        _redisClient = redisClient;
        _user = user;
    }

    public override void Configure()
    {
        Put("/extraGroup");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var outletId = await Helper.GetUserOutlet(_dbContext, _user.UserId ?? "");
        Entities.ExtraGroup? extraGroupEntity = await _dbContext.ExtraGroup.FindAsync(req.ExtraGroupId);
        if (extraGroupEntity == null)
        {
            throw new Exception("Extra Group Not Found");
        }

        Entities.OutletExtraGroup? outletExtraGroupExist = await _dbContext.OutletExtraGroup.FirstOrDefaultAsync(x => x.ExtraGroupId == req.ExtraGroupId && x.OutletId == outletId);

        extraGroupEntity.ExtraGroupId = req.ExtraGroupId;
        extraGroupEntity.OutletId = outletId;
        extraGroupEntity.Name = req.Name;

        if (req.isGlobal)
        {
            if (outletExtraGroupExist == null)
            {
                Entities.OutletExtraGroup outletExtraGroup = new()
                {
                    OutletId = outletId,
                    ExtraGroupId = req.ExtraGroupId
                };
                await _dbContext.OutletExtraGroup.AddAsync(outletExtraGroup);
            }

        }
        if (!req.isGlobal && outletExtraGroupExist != null)
        {
            _dbContext.Remove(outletExtraGroupExist);
        }

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
        // await Helper.ClearCacheOutlet(_dbContext, _redisClient, req.OutletId);

    }


}

