using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.GetAllDivision
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
            Get("/stock/division/list");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.Database.SqlQuery<Response>($"""
                SELECT
                    s.id,
                    s.outlet_id,
                    s.name,
                    s.unit_id,
                    u.name unit_name,
                    s.stock_category_id,
                    i.actual total_actual,
                    s.has_vat
                FROM
                    stock_item i
                    JOIN stock s ON s.id = i.stock_id
                    JOIN unit u ON s.unit_id = u.id
                WHERE
                    division_id = {req.DivisionId}
            """).GetPagedAsync(req, ct);

            await SendAsync(results);
        }
    }
}



