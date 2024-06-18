using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;


namespace Kayord.Pos.Features.CashUp.User.List;

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
        Get("/cashUp/user");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (_user.UserId == null)
        {
            await SendForbiddenAsync();
            return;
        }
        decimal userTotal = 0m;
        decimal tipLevyTotal = 0m;

        var listClock = await _dbContext.Clock.Where(x => x.EndDate == null && x.OutletId == req.OutletId).ToListAsync();

        var outletPayTypes = await _dbContext.OutletPaymentType.Where(x => x.OutletId == req.OutletId).Include(x => x.PaymentType).ToListAsync();
        var outletPayTypeIds = outletPayTypes.Select(x => x.PaymentTypeId).ToList();

        var payTypes = await _dbContext.PaymentType.Where(x => outletPayTypeIds.Contains(x.PaymentTypeId)).ToListAsync();

        var tableBooking = await _dbContext.TableBooking.Where(x => x.UserId == req.UserId && x.SalesPeriodId == req.SalesPeriodId && x.CashUpUserId == null).Include(x => x.Payments).Include(x => x.OrderItems).ToListAsync();

        List<int> paymentWithLevyIds = outletPayTypes.Where(x => x.PaymentType.TipLevyPercentage != 0m).Select(rd => rd.PaymentTypeId).ToList();

        List<ResponseItem> responseItems = new();
        foreach (var item in _dbContext.CashUpUserItemType.Where(x => outletPayTypeIds.Contains(x.PaymentTypeId!.Value)))
        {
            ResponseItem ri = new();
            ri.CashUpUserItemType = item;
            ri.Value = 0m;
            responseItems.Add(ri);
        }

        foreach (var item in tableBooking)
        {
            decimal tablePayments = item.Payments?.Where(x => paymentWithLevyIds.Contains(x.PaymentTypeId ?? 0)).Sum(x => x.Amount) ?? 0;
            decimal tipOverage = (tablePayments - (item.Total ?? 0)) * -1;
            decimal tipLevy = 0;
            if (tipOverage > 0)
            {
                List<PaymentType> PTUsed = payTypes.Where(x => paymentWithLevyIds.Contains(x.PaymentTypeId)).ToList();
                decimal levyTotal = PTUsed.Sum(x => x.TipLevyPercentage);
                decimal levyCount = PTUsed.Count;
                tipLevy = levyTotal / levyCount * tipOverage;
                foreach (var ptU in PTUsed)
                {
                    responseItems.FirstOrDefault(x => x.CashUpUserItemType.PaymentTypeId!.Value == ptU.PaymentTypeId)!.Value += tipOverage / levyCount * ptU.TipLevyPercentage;
                }
            }
            userTotal += item.Total ?? 0;
            tipLevyTotal += tipLevy;

        }

        Response response = new();
        response.ResponseItems = responseItems;
        response.UserId = req.UserId;
        response.User = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == req.UserId) ?? default!;
        await SendAsync(response);

    }

}
