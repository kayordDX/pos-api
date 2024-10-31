using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay;

public static class Halo
{
    public static async Task<Entities.HaloConfig> GetHaloConfig(int outletId, AppDbContext dbContext)
    {
        //TODO: Cache this somehow
        var result = await dbContext.HaloConfig
            .Where(x => x.OutletId == outletId && x.IsEnabled)
            .FirstOrDefaultAsync();
        if (result == null)
        {
            throw new Exception("Could not find outlet config");
        }
        return result;
    }
}