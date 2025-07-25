using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Data;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Pay.Status;

public class Endpoint : Endpoint<Request, Result<Dto.StatusResultDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly HaloService _halo;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, HaloService halo, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _halo = halo;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("pay/status/{reference}");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(_cu.UserId))
        {
            await Send.UnauthorizedAsync();
            return;
        }
        int outletId = await Helper.GetUserOutlet(_dbContext, _cu.UserId);
        var result = await _halo.GetStatus(r.Reference, _cu.UserId, outletId);
        await Send.OkAsync(result);
    }
}