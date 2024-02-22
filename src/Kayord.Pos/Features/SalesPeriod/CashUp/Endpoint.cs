using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.DTO;

using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.SalesPeriod.CashUp;


public class Endpoint : Endpoint<Request, CashUp>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Get("/salesperiod/cashup");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.SalesPeriod? sp = await _dbContext.SalesPeriod.FirstOrDefaultAsync(x => x.Id == req.SalesPeriodId);
        if (sp == null)
            await SendNotFoundAsync();
        List<TableCashUp> salesPeriodTableCashUps = new();
        List<UserCashUp> salesPeriodUserCashUps = new();
        UserCashUp userCashUp = new();
        CashUp cashUp = new();

        foreach (Entities.TableBooking tb in _dbContext.TableBooking.Where(x => x.SalesPeriodId == req.SalesPeriodId))
        {
            TableCashUp tableCashUp = new();
            tableCashUp.Total = 0;
            decimal TotalPayments = 0m;
            var tableBooking = await _dbContext.TableBooking.FirstOrDefaultAsync(x => x.Id == tb.Id);
            var paymentStatusIds = _dbContext.OrderItemStatus.Where(x => x.isCancelled == false).Select(rd => rd.OrderItemStatusId).ToList();
            if (tableBooking == null)
                await SendNotFoundAsync();
            tableCashUp.OrderItems = await _dbContext.OrderItem
            .Where(x => paymentStatusIds.Contains(x.OrderItemStatusId) && x.TableBookingId == tb.Id)
            .ProjectToDto()
            .ToListAsync();
            tableCashUp.UserId = tb.UserId;
            tableCashUp.PaymentsReceived = await _dbContext.Payment.Where(x => x.TableBookingId == tb.Id).ToListAsync();

            tableCashUp.Total += tableCashUp.OrderItems.Sum(item => item.MenuItem.Price);

            tableCashUp.Total += tableCashUp.OrderItems.Where(item => item.Options != null)
                                          .Sum(item => item.Options!.Sum(option => option.Price));

            tableCashUp.Total += tableCashUp.OrderItems.Where(item => item.Extras != null)
                                          .Sum(item => item.Extras!.Sum(extra => extra.Price));

            TotalPayments += tableCashUp.PaymentsReceived.Where(item => item.TableBookingId! == tb.Id)
                                          .Sum(item => item.Amount);
            tableCashUp.Balance = tableCashUp.Total - TotalPayments;

            salesPeriodTableCashUps.Add(tableCashUp);
        }
        var distinctUserIds = salesPeriodTableCashUps.Select(cashUp => cashUp.UserId).Distinct();
        foreach (var userId in distinctUserIds)
        {
            UserCashUp u = new();
            u.UserTotal = userCashUp.TableCashUps.Where(item => item.UserId! == userId)
                                          .Sum(item => item.Total);
            u.UserBalance = userCashUp.TableCashUps.Where(item => item.UserId! == userId)
                                          .Sum(item => item.Balance);
            u.TableCashUps.AddRange(userCashUp.TableCashUps.Where(item => item.UserId! == userId));
            salesPeriodUserCashUps.Add(u);
        }
        cashUp.UserCashUps.AddRange(salesPeriodUserCashUps);
        cashUp.CashUpBalance = salesPeriodUserCashUps.Sum(item => item.UserBalance);
        cashUp.CashUpTotal = salesPeriodUserCashUps.Sum(item => item.UserTotal);
        cashUp.SalesPeriodId = req.SalesPeriodId;
        await SendAsync(cashUp);
    }
}