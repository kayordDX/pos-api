using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay.Check;

public class Endpoint : Endpoint<Request, Response>
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
        Post("/pay/check");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(_cu.UserId))
        {
            await SendUnauthorizedAsync();
            return;
        }

        Response response = new();

        var haloRequests = await _dbContext.HaloReference
            .Where(x => x.TableBookingId == req.TableBookingId)
            .ToListAsync(ct);

        int outletId = await Helper.GetUserOutlet(_dbContext, _cu.UserId);

        List<string> checkedHalo = new();

        foreach (var haloRequest in haloRequests)
        {
            if (haloRequest.HaloRef != null && !checkedHalo.Contains(haloRequest.HaloRef))
            {
                await _halo.GetStatus(haloRequest.HaloRef, _cu.UserId, outletId);
                response.Checked = response.Checked + 1;
                checkedHalo.Add(haloRequest.HaloRef);
                await Task.Delay(600, ct);
            }
        }

        await SendAsync(response);
    }
}