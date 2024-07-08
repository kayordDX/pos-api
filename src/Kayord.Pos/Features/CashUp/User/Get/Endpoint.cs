using Kayord.Pos.Data;
using Kayord.Pos.Features.TableOrder.GetBill;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.CashUp.User.Get;

public class Endpoint : Endpoint<Request, Response>
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

        Response responses = new()
        {
            Items = new()
        };


        Pos.Entities.SalesPeriod? salesPeriod = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.OutletId == req.OutletId && x.EndDate == null);
        if (salesPeriod != null)
        {
            foreach (var item in listClock)
            {
                decimal sales = 0;
                decimal tips = 0;
                decimal totalPayments = 0;
                int openTableCount = 0;
                var bookings = await _dbContext.TableBooking.Where(x => x.SalesPeriodId == salesPeriod.Id && x.UserId == item.UserId && x.CashUpUserId == null).ToListAsync();

                foreach (var b in bookings)
                {
                    TableTotal bill = await Bill.GetTotal(b.Id, _dbContext);
                    sales += bill.Total;
                    tips += bill.TipTotal;
                    totalPayments += bill.TotalPayments;
                    openTableCount += b.CloseDate == null ? 1 : 0;
                }
                Pos.Entities.User? u = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                if (u != null)
                {
                    Items r = new();
                    r.Sales = sales;
                    r.Tips = tips;
                    r.Payments = totalPayments;
                    r.User = u;
                    r.UserId = u.UserId;
                    r.OpenTableCount = openTableCount;
                    responses.Items.Add(r);
                }
            }
        }
        responses.TotalPayments = responses.Items.Sum(x => x.Payments);
        responses.TotalTips = responses.Items.Sum(x => x.Tips);
        responses.TotalSales = responses.Items.Sum(x => x.Sales);


        await SendAsync(responses);

    }

}
