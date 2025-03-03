using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Items.GetAll
{
    public class Endpoint : Endpoint<Request, List<Response>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/stock/items");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.Database.SqlQuery<Response>($"""
                SELECT
                    coalesce(i."Id", 0) "Id",
                coalesce(s."Id", 0) "StockId",
                coalesce(s."Name", '') "StockName",
                d."DivisionId",
                d."DivisionName",
                coalesce(i."Threshold", 0) "Threshold",
                coalesce(i."Actual", 0) "Actual"
                FROM "Stock" s
                LEFT JOIN "Division" d
                    ON s."OutletId" = d."OutletId"
                LEFT JOIN "StockItem" i
                    ON i."StockId" = s."Id"
                AND i."DivisionId" = d."DivisionId"
                WHERE s."Id" = {req.Id}
            """).ToListAsync(ct);

            await SendAsync(results);
        }
    }
}



