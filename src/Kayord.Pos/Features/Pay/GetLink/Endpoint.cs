using Kayord.Pos.Common.Wrapper;
using Kayord.Pos.Data;
using Kayord.Pos.Events;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay.GetLink;

public class Endpoint : Endpoint<Request, Result<Response>>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;
    private readonly HaloService _halo;

    public Endpoint(AppDbContext dbContext, HaloService halo, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _halo = halo;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("/pay/getLink");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(_cu.UserId))
        {
            await Send.UnauthorizedAsync();
            return;
        }
        var userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _cu.UserId && x.IsCurrent);
        if (userOutlet == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        var results = await _halo.GetLink(req.Amount, req.TableBookingId, _cu.UserId, userOutlet.OutletId);
        if (!results.Failure)
        {
            await PublishAsync(new PayLinkReceivedEvent
            {
                url = results.Value.url,
                reference = results.Value.reference,
                UserId = _cu.UserId,
                OutletId = userOutlet.OutletId
            }, Mode.WaitForNone);
        }
        await Send.OkAsync(results);
    }
}