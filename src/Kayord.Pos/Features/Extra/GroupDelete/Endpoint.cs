using Kayord.Pos.Data;
using Kayord.Pos.Data.Migrations;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Extra.GroupDelete;

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
        Delete("/extraGroup{id}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Extra? existingExtra = await _dbContext.Extra.FirstOrDefaultAsync(x => x.ExtraGroupId == req.Id);

        if (existingExtra != null)
        {
            throw new Exception("Cannot Delete Group Containing Extras");
        }

        Entities.ExtraGroup? extraGroup = await _dbContext.ExtraGroup.FindAsync(req.Id);

        if (extraGroup == null)
        {
            throw new Exception("Extra Group Not Found");
        }
        var outletId = await Helper.GetUserOutlet(_dbContext, _user.UserId ?? "");

        Entities.OutletExtraGroup? outletExtraGroup = await _dbContext.OutletExtraGroup.FirstOrDefaultAsync(x => x.ExtraGroupId == req.Id && x.OutletId == outletId);
        if (outletExtraGroup != null)
        {
            _dbContext.OutletExtraGroup.Remove(outletExtraGroup);
        }
        _dbContext.ExtraGroup.Remove(extraGroup);

        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
        // await Helper.ClearCacheOutlet(_dbContext, _redisClient, req.OutletId);

    }


}

