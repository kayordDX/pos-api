using Kayord.Pos.Data;
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
}