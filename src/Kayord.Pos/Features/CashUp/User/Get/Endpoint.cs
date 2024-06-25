using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.CashUp.User.Get;

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
        Get("/cashUp/user/{outletId}");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (_user.UserId == null)
        {
            await SendForbiddenAsync();
            return;
        }

        var listClock = await _dbContext.Clock.Where(x => x.EndDate == null && x.OutletId == req.OutletId).ToListAsync();

        List<Response> responses = new();
        decimal sales = 0;
        decimal tips = 0;
        Pos.Entities.SalesPeriod? salesPeriod = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.OutletId == req.OutletId && x.EndDate == null)
        if (salesPeriod != null)
        {
            foreach (var item in listClock)
            {
                var bookings = await _dbContext.TableBooking.Where(x => x.SalesPeriodId == salesPeriod.Id && x.UserId == item.UserId && x.CashUpUserId == null).ToListAsync();

                foreach (var table in bookings)
                {

                }
                if (u != null)
                {
                    Response r = new();
                    r.Sales = 2000;
                    r.Tips = 200;
                    r.User = u;
                    responses.Add(r);
                }
            }
        }
        await SendAsync(responses);

    }

}
