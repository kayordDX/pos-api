using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill;
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

        List<string> userIds = await _dbContext.TableBooking
            .Include(x => x.SalesPeriod)
            .Where(x => x.CashUpUserId == null && x.SalesPeriod.OutletId == req.OutletId)
            .Select(x => x.UserId)
            .Distinct()
            .ToListAsync();

        Response responses = new()
        {
            Items = new()
        };

        Entities.SalesPeriod? salesPeriod = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.OutletId == req.OutletId && x.EndDate == null);
        if (salesPeriod != null)
        {
            var todoItems = await GetUserCashUpItems(userIds, salesPeriod.Id, _dbContext, req.OutletId);
            responses.Items.AddRange(todoItems);

            var cashedUpUserIds = await _dbContext.CashUpUser
                .Where(x => x.OutletId == req.OutletId)
                .Where(x => x.SalesPeriodId == salesPeriod.Id)
                .Select(x => x.UserId)
                .ToListAsync();

            var cashedUpItems = await GetUserCashUpItems(cashedUpUserIds, salesPeriod.Id, _dbContext, req.OutletId, true);
            responses.Items.AddRange(cashedUpItems);
        }
        responses.TotalPayments = responses.Items.Sum(x => x.Payments);
        responses.TotalTips = responses.Items.Sum(x => x.Tips);
        responses.TotalSales = responses.Items.Sum(x => x.Sales);


        await SendAsync(responses);

    }

    public static async Task<List<Items>> GetUserCashUpItems(List<string> userIds, int salesPeriodId, AppDbContext _dbContext, int outletId, bool isCashedUp = false)
    {
        List<Items> items = new();
        foreach (var userId in userIds)
        {
            decimal sales = 0;
            decimal tips = 0;
            decimal totalPayments = 0;
            int openTableCount = 0;
            int userCashUpId = 0;

            var booking = _dbContext.TableBooking
                .Where(x => x.SalesPeriodId == salesPeriodId)
                .Where(x => x.UserId == userId);

            if (!isCashedUp)
            {
                booking = booking.Where(x => x.CashUpUserId == null);
            }
            else
            {
                booking = booking.Where(x => x.CashUpUserId != null);
            }

            if (isCashedUp)
            {
                var cashUp = await _dbContext.CashUpUser
                    .Where(x => x.OutletId == outletId)
                    .Where(x => x.UserId == userId)
                    .Where(x => x.SalesPeriodId == salesPeriodId)
                    .FirstOrDefaultAsync();
                userCashUpId = cashUp?.Id ?? 0;
            }
            var bookings = await booking.ToListAsync();

            foreach (var b in bookings)
            {
                sales += b?.Total ?? 0;
                tips += b?.TotalTips ?? 0;
                totalPayments += b?.TotalPayments ?? 0;
                openTableCount += b?.CloseDate == null ? 1 : 0;
            }
            Entities.User? u = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == userId);
            if (u != null)
            {
                Items r = new Items
                {
                    Sales = sales,
                    Tips = tips,
                    Payments = totalPayments,
                    User = u,
                    UserId = u.UserId,
                    OpenTableCount = openTableCount,
                    CashUpUserId = userCashUpId
                };
                items.Add(r);
            }
        }
        return items;
    }

}
