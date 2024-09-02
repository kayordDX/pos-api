

using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Menu.List
{
    public class Endpoint : Endpoint<Request, List<Entities.Menu>>
    {
        private readonly Data.AppDbContext _dbContext;
        private readonly RedisClient _redisClient;

        public Endpoint(Data.AppDbContext dbContext, RedisClient redisClient)
        {
            _dbContext = dbContext;
            _redisClient = redisClient;
        }

        public override void Configure()
        {
            Get("/menu");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            string cacheKey = $"menu:getAll:{req.OutletId}";
            var cachedResponse = await _redisClient.GetObjectAsync<List<Entities.Menu>>(cacheKey);
            if (cachedResponse != null)
            {
                await SendAsync(cachedResponse);
                return;
            }

            var menus = await _dbContext.Menu
                .Where(menu => menu.OutletId == req.OutletId)
                .OrderBy(x => x.Position)
                .ToListAsync();

            await _redisClient.SetObjectAsync(cacheKey, menus);
            await SendAsync(menus);
        }
    }
}