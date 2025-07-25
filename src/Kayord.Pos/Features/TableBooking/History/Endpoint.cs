using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.History;

public class Endpoint : Endpoint<Request, List<Response>>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _user;

    public Endpoint(AppDbContext dbContext, CurrentUserService user)
    {
        _dbContext = dbContext;
        _user = user;
    }

    public override void Configure()
    {
        Get("/tableBooking/myHistory");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var bookings = _dbContext.TableBooking
            .Where(x => x.UserId == _user.UserId)
            .Where(x => x.CloseDate != null);

        if (r.TableBookingId > 0)
        {
            bookings = bookings.Where(x => x.Id.ToString().StartsWith(r.TableBookingId.ToString()));
        }

        var result = await bookings
            .OrderByDescending(x => x.CloseDate)
            .ProjectToDto()
            .ToListAsync();

        await Send.OkAsync(result);
    }
}