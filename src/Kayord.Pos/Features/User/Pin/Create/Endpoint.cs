using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.Pin.Create;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;
    private readonly EncryptionService _encryption;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu, EncryptionService encryption)
    {
        _dbContext = dbContext;
        _cu = cu;
        _encryption = encryption;
    }

    public override void Configure()
    {
        Post("/user/pin");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var userOutlet = await _dbContext.UserOutlet
            .Select(x => new { x.Id, x.IsCurrent, x.UserId, OutletId = x.Outlet.Id, OutletName = x.Outlet.Name })
            .FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent);

        if (userOutlet == null)
        {
            ValidationContext.Instance.ThrowError("Could not find outlet for user");
        }

        var userOutletPin = await _dbContext.UserOutletPin
            .Where(x => x.UserId == _cu.UserId && x.OutletId == userOutlet.OutletId)
            .FirstOrDefaultAsync(ct);


        var iv = _encryption.GenerateIV();
        if (userOutletPin == null)
        {
            userOutletPin = new UserOutletPin
            {
                IsEnabled = req.IsEnabled,
                OutletId = userOutlet.OutletId,
                UserId = _cu.UserId ?? "",
                Pin = _encryption.Encrypt(req.Pin, iv),
                Iv = iv
            };
            await _dbContext.UserOutletPin.AddAsync(userOutletPin, ct);
        }
        else
        {
            userOutletPin.IsEnabled = req.IsEnabled;
            userOutletPin.Pin = _encryption.Encrypt(req.Pin, iv);
            userOutletPin.Iv = iv;
        }

        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}