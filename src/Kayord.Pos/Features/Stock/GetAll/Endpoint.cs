using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.GetAll
{
    public class Endpoint : Endpoint<Request, PaginatedList<Response>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/stock");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.Database.SqlQuery<Response>($"""
                SELECT 
                    s."Id", 
                    s."OutletId",
                    s."Name",
                    s."UnitId",
                    u."Name" "UnitName",
                    s."StockCategoryId",
                    coalesce(sum(i."Actual"),0) "TotalActual"
                FROM "Stock" s
                LEFT JOIN "StockItem" i
                    ON s."Id" = i."StockId"
                JOIN "Unit" u
                    ON s."UnitId" = u."Id"
                WHERE s."OutletId" = {req.OutletId}   
                GROUP BY s."Id", u."Name"
            """).GetPagedAsync(req, ct);

            await SendAsync(results);
        }
    }
}



