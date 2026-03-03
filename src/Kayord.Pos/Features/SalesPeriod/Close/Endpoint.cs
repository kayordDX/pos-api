using Kayord.Pos.Data;
using Kayord.Pos.Events;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.SalesPeriod.Close;

public class Endpoint : Endpoint<Request, Entities.SalesPeriod>
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
        Post("/salesPeriod/close");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.SalesPeriod.FindAsync(req.SalesPeriodId);
        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }
        var OpenTableCount = await _dbContext.TableBooking.Where(x => x.SalesPeriodId == req.SalesPeriodId && x.CloseDate == null).CountAsync();
        if (OpenTableCount > 0)
        {
            ValidationContext.Instance.ThrowError("Cannot close sales period with open tables");
        }

        // Check if any in progress orders
        OrderResult? inProgressOrderCount = await _dbContext.Database.SqlQuery<OrderResult>($"""
            SELECT
                count(order_item_id) count
            FROM order_item oi
            JOIN order_item_status ois
                ON oi.order_item_status_id = ois.order_item_status_id
            JOIN table_booking tb
                ON tb.id = oi.table_booking_id
            JOIN sales_period s
                ON s.id = tb.sales_period_id
            WHERE ois.is_back_office = true
            AND s.outlet_id = {entity.OutletId}
        """).FirstOrDefaultAsync(ct);

        if (inProgressOrderCount?.Count > 0)
        {
            ValidationContext.Instance.ThrowError("Cannot close sales period with open orders");
        }

        entity.EndDate = DateTime.Now;

        // Clock out all users
        _dbContext.Clock
            .Where(x => x.OutletId == entity.OutletId && x.EndDate == null)
            .ToList()
            .ForEach(x => x.EndDate = DateTime.Now);

        await _dbContext.SaveChangesAsync(ct);

        await new StockPeriodSnapshotEvent
        {
            SalesPeriodId = entity.Id,
            OutletId = entity.OutletId,
            CreatedBy = _cu.UserId ?? ""
        }.PublishAsync(Mode.WaitForNone, ct);

        await Send.OkAsync(entity);
    }
}
