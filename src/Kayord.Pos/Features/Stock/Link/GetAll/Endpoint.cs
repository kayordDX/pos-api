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
            var results = await _dbContext.Database.SqlQuery<Response>($"""
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
            """).ToListAsync(ct);

            await SendAsync(results);
        }
    }
}



