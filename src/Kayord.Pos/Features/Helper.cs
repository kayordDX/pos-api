using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features;

public static class Helper
{
    public static async Task<int> GetUserOutlet(AppDbContext dbContext, string userId)
    {
        var userOutlet = await dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == userId && x.IsCurrent);
        if (userOutlet == null)
        {
            throw new Exception("Could not find outlet for user");
        }
        return userOutlet.OutletId;
    }

    public static async Task ClearCacheOutlet(AppDbContext dbContext, RedisClient redisClient, int outletId)
    {
        // Delete all cache -> menu:getAll:1
        string all = $"menu:getAll:{outletId}";
        await redisClient.DeletePatternAsync(all);

        // Delete Menu Sections
        // menu:sections:1:1
        // menu:items:1:1:search
        var menus = await dbContext.Menu
            .Where(x => x.OutletId == outletId)
            .Select(x => x.Id)
            .ToListAsync();

        foreach (var menu in menus)
        {
            string sections = $"menu:sections:{menu}:*";
            string items = $"menu:items:{menu}:*";
            await redisClient.DeletePatternAsync(sections);
            await redisClient.DeletePatternAsync(items);
        }
    }
}