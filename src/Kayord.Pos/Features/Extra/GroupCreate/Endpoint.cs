using Kayord.Pos.Data;
using Kayord.Pos.Data.Migrations;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Extra.GroupCreate;

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
        Post("/extraGroup");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var outletId = await Helper.GetUserOutlet(_dbContext, _user.UserId ?? "");

        Entities.ExtraGroup? extraGroup = new()
        {
            Name = req.Name,
            OutletId = outletId
        };

        await _dbContext.ExtraGroup.AddAsync(extraGroup);

        if (req.isGlobal)
        {
            Entities.OutletExtraGroup outletExtraGroup = new()
            {
                OutletId = extraGroup.OutletId,
                ExtraGroupId = extraGroup.ExtraGroupId
            };
            await _dbContext.OutletExtraGroup.AddAsync(outletExtraGroup);
        }
        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
        // await Helper.ClearCacheOutlet(_dbContext, _redisClient, req.OutletId);

    }


}

