using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Extra.Delete;

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
        Delete("/extra/{id}");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {

        Entities.Extra? extra = await _dbContext.Extra.FindAsync(req.Id);

        if (extra != null)
        {
            _dbContext.Extra.Remove(extra);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Extra Not Found");
        }
    }

}


