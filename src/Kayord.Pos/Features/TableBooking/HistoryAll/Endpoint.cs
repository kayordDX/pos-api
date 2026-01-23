using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.Features.TableBooking.History;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.HistoryAll;

public class Endpoint(AppDbContext dbContext, UserService user) : Endpoint<Request, PaginatedList<Response>>
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly UserService _user = user;

    public override void Configure()
    {
        Get("/tableBooking/history/all");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        int outletId = await _user.GetOutletId();
        var booking = _dbContext.TableBooking.Where(x => x.SalesPeriod.OutletId == outletId).AsNoTracking();

        if (req.StartDate.HasValue)
        {
            booking = booking.Where(x => x.CloseDate >= req.StartDate.Value);
        }
        if (req.EndDate.HasValue)
        {
            booking = booking.Where(x => x.CloseDate <= req.EndDate.Value);
        }

        if (req.TableBookingId > 0)
        {
            booking = booking.Where(x => x.Id.ToString().StartsWith(req.TableBookingId.ToString()));
        }

        var result = await booking.OrderByDescending(x => x.CloseDate)
            .ProjectToDto()
            .GetPagedAsync(req, ct);

        await Send.OkAsync(result, ct);
    }
}
