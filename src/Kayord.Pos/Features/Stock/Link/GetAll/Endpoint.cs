using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Link.GetAll;

public class Endpoint : Endpoint<Request, List<Response>>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/stock/link/all");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        IQueryable<Response>? query = null;
        if (req.LinkType == 0)
        {
            query = _dbContext.Database.SqlQuery<Response>($"""
            select
                i.menu_item_id id, 
                i.stock_id,
                s.name, 
                s.unit_id, 
                u.name unit_name,
                i.quantity,
                coalesce(sum(si.actual),0) total_actual
            from menu_item_stock i
            join stock s
                on i.stock_id = s.id
            join unit u
                on u.id = s.unit_id
            left join stock_item si
                on s.id = si.stock_id
            where i.menu_item_id = {req.Id}
            group by
                i.menu_item_id,
                i.stock_id,
                s.name,
                s.unit_id,
                u.name,
                i.quantity
            """);
        }
        else if (req.LinkType == 1)
        {
            query = _dbContext.Database.SqlQuery<Response>($"""
                select 
                    i.extra_id id, 
                    i.stock_id, 
                    s.name, 
                    s.unit_id, 
                    u.name unit_name,
                    i.quantity,
                    coalesce(sum(si.actual),0) total_actual
                from extra_stock i
                join stock s
                    on i.stock_id = s.id
                join unit u
                    on u.id = s.unit_id
                left join stock_item si
                    on s.id = si.stock_id
                where i.extra_id = {req.Id}
                group by 
                    i.extra_id, 
                    i.stock_id, 
                    s.name, 
                    s.unit_id, 
                    u.name,
                    i.quantity
            """);
        }
        else if (req.LinkType == 2)
        {
            query = _dbContext.Database.SqlQuery<Response>($"""
                select 
                    i.option_id id, 
                    i.stock_id, 
                    s.name, 
                    s.unit_id, 
                    u.name unit_name,
                    i.quantity,
                        coalesce(sum(si.actual),0) total_actual
                from option_stock i
                join stock s
                    on i.stock_id = s.id
                join unit u
                    on u.id = s.unit_id
                left join stock_item si
                    on s.id = si.stock_id
                where i.option_id = {req.Id} 
                group by
                    i.option_id, 
                    i.stock_id, 
                    s.name, 
                    s.unit_id, 
                    u.name,
                    i.quantity
            """);
        }
        else if (req.LinkType == 3)
        {
            query = _dbContext.Database.SqlQuery<Response>($"""
                select
                    i.menu_item_id id, 
                    i.stock_id,
                    s.name, 
                    s.unit_id, 
                    u.name unit_name,
                    i.quantity,
                    coalesce(sum(si.actual),0) total_actual
                from menu_item_bulk_stock i
                join stock s
                    on i.stock_id = s.id
                join unit u
                    on u.id = s.unit_id
                left join stock_item si
                    on s.id = si.stock_id
                where i.menu_item_id = {req.Id}
                group by
                i.menu_item_id, 
                    i.stock_id,
                    s.name, 
                    s.unit_id, 
                    u.name,
                    i.quantity
            """);
        }

        if (query == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        var results = await query.ToListAsync(ct);
        await Send.OkAsync(results);
    }
}



