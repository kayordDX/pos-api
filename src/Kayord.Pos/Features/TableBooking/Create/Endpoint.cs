using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.Create;

public class Endpoint : Endpoint<Request, Entities.TableBooking>
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
        Post("/tableBooking");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (_user.UserId == null)
        {
            await SendForbiddenAsync();
            return;
        }

        Entities.SalesPeriod? salesPeriod = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.Id == req.SalesPeriodId);

        if (salesPeriod!.EndDate != null)
        {
            await SendNotFoundAsync();
            return;
        }

        // Check if already open booking for this table.
        var existing = await _dbContext.TableBooking.FirstOrDefaultAsync(x => x.TableId == req.TableId && x.SalesPeriodId == req.SalesPeriodId && x.CloseDate == null, ct);
        if (existing != null)
        {
            ValidationContext.Instance.ThrowError("This table is already booked");
        }

        Entities.TableBooking entity = new()
        {
            TableId = req.TableId,
            BookingName = req.BookingName,
            SalesPeriodId = req.SalesPeriodId,
            UserId = _user.UserId
        };

        await _dbContext.TableBooking.AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
        await SendAsync(entity);
    }
}