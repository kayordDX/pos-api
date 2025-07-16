using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stats.PaymentTypes;

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
        Get("/stats/paymentTypes");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var userOutlet = await _dbContext.UserOutlet.FirstOrDefaultAsync(x => x.UserId == _user.UserId && x.IsCurrent);
        if (userOutlet == null)
        {
            await SendNotFoundAsync();
            return;
        }

        var results = await _dbContext.Database.SqlQuery<Response>($"""
            SELECT 
                a.payment_type,
                a.amount,
                b.amount::integer average_amount
            FROM (
                SELECT 
                    pt.payment_type_name payment_type,
                    SUM(p.amount) amount
                FROM payment p
                JOIN payment_type pt 
                    ON p.payment_type_id = pt.payment_type_id
                JOIN table_booking tb 
                ON tb.id = p.table_booking_id
                JOIN sales_period sp 
                ON tb.sales_period_id = sp.id
                WHERE tb.sales_period_id = {r.SalesPeriodId}
                AND sp.outlet_id = {userOutlet.OutletId}
                GROUP BY pt.payment_type_name
            ) a
            JOIN (
                SELECT 
                    pt.payment_type_name payment_type,
                    AVG(p.amount) amount
                FROM payment p
                JOIN payment_type pt 
                    ON p.payment_type_id = pt.payment_type_id
                JOIN table_booking tb 
                ON tb.id = p.table_booking_id
                JOIN sales_period sp 
                ON tb.sales_period_id = sp.id
                WHERE tb.sales_period_id IN (select id from sales_period where outlet_id = {userOutlet.OutletId} order by id desc limit 5)
                AND sp.outlet_id = {userOutlet.OutletId}
                GROUP BY pt.payment_type_name
            ) b
            ON a.payment_type = b.payment_type
            """).ToListAsync(ct);

        await SendAsync(results);
    }
}