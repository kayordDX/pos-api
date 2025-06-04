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
            Policies(Constants.Policy.Manager);
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.Database.SqlQuery<Response>($"""
                select
                    coalesce(i."id", 0) "id",
                coalesce(s."id", 0) "stock_id",
                coalesce(s."name", '') "stock_name",
                d."division_id",
                d."division_name",
                coalesce(i."threshold", 0) "threshold",
                coalesce(i."actual", 0) "actual"
                from "stock" s
                left join "division" d
                    on s."outlet_id" = d."outlet_id"
                left join "stock_item" i
                    on i."stock_id" = s."id"
                and i."division_id" = d."division_id"
                where s."id" = {req.Id}
            """).ToListAsync(ct);

            await SendAsync(results);
        }
    }
}



