using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.Pin.Login;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly UserService _userService;
    private readonly AppDbContext _dbContext;
    private readonly EncryptionService _encryption;

    public Endpoint(UserService userService, AppDbContext dbContext, EncryptionService encryption)
    {
        _userService = userService;
        _dbContext = dbContext;
        _encryption = encryption;
    }

    public override void Configure()
    {
        Post("/user/pin/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        // Check if feature is enabled for outlet
        var hasFeature = await _dbContext.OutletFeature.Where(x => x.OutletId == r.OutletId && x.FeatureId == 5).FirstOrDefaultAsync(ct);
        if (hasFeature == null)
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        // Check if User Exists
        var userPin = await _dbContext.UserOutletPin.Where(x => x.UserId == r.UserId && x.OutletId == r.OutletId).FirstOrDefaultAsync(ct);
        if (userPin == null)
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        // Check if pin matches
        if (r.Pin != _encryption.Decrypt(userPin.Pin, userPin.Iv))
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        // Generate token
        var token = await _userService.GetCustomToken(r.UserId);
        Response result = new()
        {
            Token = token
        };
        await Send.OkAsync(result);
    }
}