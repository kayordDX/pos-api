using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Models;
using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.GetAll;

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
            select 
                s."id", 
                s."outlet_id",
                s."name",
                s."unit_id",
                u."name" "unit_name",
                s."stock_category_id",
                coalesce(sum(i."actual"),0) "total_actual",
                s.has_vat,
                c.display_name category_display_name
            from "stock" s
            left join "stock_item" i
                on s."id" = i."stock_id"
            join "unit" u
                on s."unit_id" = u."id"
            left join vw_stock_category c
                on c.id = s."stock_category_id"
            where s."outlet_id" = {req.OutletId} 
            group by 
                s."id", 
                s."outlet_id",
                s."name",
                s."unit_id",
                u."name",
                u."name", 
                s."stock_category_id",
                s.has_vat,
                c.display_name
            order by id
        """).GetPagedAsync(req, ct);

        await SendAsync(results);
    }
}



