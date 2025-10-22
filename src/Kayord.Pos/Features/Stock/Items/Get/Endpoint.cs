using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Items.Get;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/stock/items/{stockId}/{divisionId}");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.Database.SqlQuery<Response>($"""
            SELECT
                i.id,
                stock_id,
                s.name stock_name,
                s.unit_id,
                u.name unit_name,
                i.division_id,
                d.division_name,
                threshold,
                actual
            FROM
                stock_item i
                JOIN stock s ON s.id = i.stock_id
                JOIN unit u ON s.unit_id = u.id
                JOIN division d ON d.division_id = i.division_id
            WHERE
                i.stock_id = {req.StockId}
                AND i.division_id = {req.DivisionId}
                AND d.is_deleted = FALSE
        """).FirstOrDefaultAsync(ct);

        if (results == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        await Send.OkAsync(results);
    }
}



