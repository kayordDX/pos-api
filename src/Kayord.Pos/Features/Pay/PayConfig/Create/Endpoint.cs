using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay.PayConfig.Create;

public class Endpoint : Endpoint<Request, Entities.HaloConfig>
{
    private readonly AppDbContext _dbContext;
    private readonly EncryptionService _encryption;

    public Endpoint(AppDbContext dbContext, EncryptionService encryption)
    {
        _dbContext = dbContext;
        _encryption = encryption;
    }

    public override void Configure()
    {
        Post("/pay/config");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var iv = _encryption.GenerateIV();
        Entities.HaloConfig entity = new Entities.HaloConfig()
        {
            MerchantId = req.MerchantId,
            XApiKey = _encryption.Encrypt(req.XApiKey, iv),
            OutletId = req.OutletId,
            Iv = iv
        };
        await _dbContext.HaloConfig.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.HaloConfig.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(result);
    }
}