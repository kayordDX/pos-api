using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.Pin.Get;

public class Endpoint : EndpointWithoutRequest<Response>
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
        Get("/user/pin");
    }

    public override async Task HandleAsync(CancellationToken ct)
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

        if (userOutletPin == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        Response response = new();
        response.IsEnabled = userOutletPin.IsEnabled;
        response.OutletId = userOutlet.OutletId;
        response.UserId = userOutletPin.UserId;
        response.Pin = _encryption.Decrypt(userOutletPin.Pin, userOutletPin.Iv);

        await Send.OkAsync(response);
    }
}
