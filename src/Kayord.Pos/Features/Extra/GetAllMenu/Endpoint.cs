using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Extra.GetAllMenu;

public class Endpoint : Endpoint<Request, List<SpecialExtrasDTO>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/extra/menu");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.Database.SqlQuery<SpecialExtrasDTO>($"""
            select
                o.extra_id,
                o.name,
                o.position_id,
                o.price,
                eg.extra_group_id,
                eg.name "extra_group_name",
                coalesce(min(((si.actual - oo.quantity) >= 0)::int)::bool, true) is_available
            from extra o
            left join extra_stock oo
                on oo.extra_id = o.extra_id
            join outlet_extra_group oeg
                    on oeg.extra_group_id =o.extra_group_id
            join stock_item si
                on si.stock_id = oo.stock_id
            join extra_group eg
                    on eg.extra_group_id =o.extra_group_id
            where oeg.outlet_id = {req.OutletId} and si.division_id = {req.DivisionId}
            group by o.extra_id,
                o.name,
                o.position_id,
                o.price,
                eg.extra_group_id,
                eg.name
            order by o.name;
        """).ToListAsync(ct);
        await Send.OkAsync(results);
    }
}