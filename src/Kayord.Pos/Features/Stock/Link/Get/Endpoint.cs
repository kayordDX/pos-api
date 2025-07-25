using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Link.Get;

public class Endpoint : Endpoint<Request, List<Response>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/stock/link/{stockId}");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var results = await _dbContext.Database.SqlQuery<Response>($"""

        select name, description, type, quantity from (
            select m.name, ms.name description, 'Menu Item' type, s.quantity 
            from menu_item_stock s
            join menu_item m
            on m.menu_item_id = s.menu_item_id
            join menu_section ms
            on ms.menu_section_id = m.menu_section_id
            where s.stock_id = {req.StockId}
            union
            select m.name, ms.name description, 'Bulk' type, s.quantity 
            from menu_item_bulk_stock s
            join menu_item m
            on m.menu_item_id = s.menu_item_id
            join menu_section ms
            on ms.menu_section_id = m.menu_section_id
            where s.stock_id = {req.StockId}
            union
            select e.name, eg.name description, 'Extra' type, s.quantity
            from extra_stock s
            join extra e
            on e.extra_id = s.extra_id
            join extra_group eg
            on e.extra_group_id = eg.extra_group_id
            where s.stock_id = {req.StockId}
            union
            select o.name, og.name description, 'Option' type, s.quantity
            from option_stock s
            join option o
            on o.option_id = s.option_id
            join option_group og
            on og.option_group_id = o.option_group_id
            where s.stock_id = {req.StockId} 
        )
        """).ToListAsync(ct);

        await Send.OkAsync(results);
    }
}



