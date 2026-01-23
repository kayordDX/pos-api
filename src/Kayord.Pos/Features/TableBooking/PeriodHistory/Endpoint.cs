using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.Features.TableBooking.History;

namespace Kayord.Pos.Features.TableBooking.PeriodHistory;

public class Endpoint(AppDbContext dbContext) : Endpoint<Request, PaginatedList<Response>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Get("/tableBooking/history/salesPeriod/{salesPeriodId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var booking = _dbContext.TableBooking
            .Where(x => x.SalesPeriodId == req.SalesPeriodId);

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
