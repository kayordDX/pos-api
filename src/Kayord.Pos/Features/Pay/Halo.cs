using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay;

public static class Halo
{
    public static async Task<Entities.HaloConfig> GetHaloConfig(int outletId, AppDbContext dbContext, EncryptionService encryption)
    {
        //TODO: Cache this somehow
        var result = await dbContext.HaloConfig
            .Where(x => x.OutletId == outletId && x.IsEnabled)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (result == null)
        {
            throw new Exception("Could not find outlet config");
        }
        result.XApiKey = encryption.Decrypt(result.XApiKey, result.Iv);
        return result;
    }

    public static async Task<Entities.HaloConfig> GetHaloSpecificConfig(int configId, AppDbContext dbContext, EncryptionService encryption)
    {
        var result = await dbContext.HaloConfig
            .Where(x => x.Id == configId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (result == null)
        {
            throw new Exception("Could not find config");
        }
        result.XApiKey = encryption.Decrypt(result.XApiKey, result.Iv);
        return result;
    }
}