using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Kayord.Pos.DTO;

using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Kayord.Pos.Features.SalesPeriod.CreateCashUp;


public class Endpoint : Endpoint<Request, Entities.CashUp>
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
        Get("/salesperiod/createCashup");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.CashUp cashUp = new();
        cashUp.CashUpBalance = req.CashUpBalance;
        cashUp.CashUpTotal = req.CashUpTotal;
        cashUp.CashUpTotalPayments = req.CashUpTotalPayments;
        cashUp.SalesPeriodId = req.SalesPeriodId;
        cashUp.TableCount = req.TableCount;
        cashUp.SignOffUserId = _cu.UserId ?? "";
        cashUp.UserId = req.UserId;

        if (cashUp.SalesPeriodId != 0)
        {
            await _dbContext.CashUp.AddAsync(cashUp);
            await _dbContext.SaveChangesAsync();
        }

        await SendAsync(cashUp);
    }
}