using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Stock.Category
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
            Get("/stock/category/{OutletId}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var results = await _dbContext.Database.SqlQuery<Response>($"""
                select 
                    id, 
                    name, 
                    parent_id, 
                    parent_name, 
                    outlet_Id, 
                    display_name 
                from 
                    vw_stock_category 
                where outlet_id = {req.OutletId} and parent_name is not null 
            """).ToListAsync(ct);
            await SendAsync(results);
        }
    }
}



