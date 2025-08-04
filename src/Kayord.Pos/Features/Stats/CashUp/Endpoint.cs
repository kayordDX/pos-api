using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stats.CashUp;

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
        Get("/stats/cashUp");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _user.UserId && x.IsCurrent);
        if (userOutlet == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        var results = await _dbContext.Database.SqlQuery<Response>($"""
            SELECT
                t.name,
                coalesce(sum(t.item_price_final) - sum(coalesce(a.adjustment_amount,0)),0) revenue,
                coalesce(sum(t.item_price_final),0) actual_sales,
                sum(coalesce(a.adjustment_amount,0)) adjustments,
                coalesce(sum(total_tips),0) tips,
                round(coalesce(sum(total_tips),0) / (CASE WHEN coalesce(sum(t.item_price_final),0) = 0 THEN 1 ELSE sum(t.item_price_final) END)*100, 2) as tips_percentage,
                coalesce(sum(total_payments),0) payments
            from
            (
                select
                    tb.id,
                    u.name,
                    count(*) orders,
                    sum(tb.total) as item_price_final,
                    sum(tb.total_tips) as total_tips,
                    sum(tb.total_payments) as total_payments
                from  table_booking tb
                join "user" u 
                    on tb.user_id = u.user_id
                join sales_period sp 
                    on sp.id = tb.sales_period_id
                where sp.outlet_id = {userOutlet.OutletId}
                and  tb.sales_period_id = {r.SalesPeriodId}
                    group by
                    tb.id,
                    u.name
            ) t 
            LEFT JOIN vw_adjustment a on a.table_booking_id = t.id
            group by t.name
            """).ToListAsync(ct);

        await Send.OkAsync(results);
    }
}