using Kayord.Pos.Data;
using Kayord.Pos.Features.TableBooking.History;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.PeriodHistory;

public class Endpoint : Endpoint<Request, List<Response>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
            .ToListAsync();

        await SendAsync(result);
    }
}