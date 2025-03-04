using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Link.GetAll
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
            Get("/stock/link/all");
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
                        u.name unit_name
                    from menu_item_stock i
                    join stock s
                    on i.stock_id = s.id
                    join unit u
                    on u.id = s.unit_id
                    where i.menu_item_id = {req.Id}
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
                        u.name unit_name
                    from extra_stock i
                    join stock s
                    on i.stock_id = s.id
                    join unit u
                    on u.id = s.unit_id
                    where i.extra_id = {req.Id}
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
                        u.name unit_name
                    from option_stock i
                    join stock s
                    on i.stock_id = s.id
                    join unit u
                    on u.id = s.unit_id
                    where i.option_id = {req.Id}
                """);
            }

            if (query == null)
            {
                await SendNotFoundAsync();
                return;
            }

            var results = await query.ToListAsync(ct);
            await SendAsync(results);
        }
    }
}



