using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Outlet.List
{
    public class Endpoint : EndpointWithoutRequest<List<Pos.Entities.Outlet>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/outlet");
            AllowAnonymous();
        }

         public override async Task HandleAsync(CancellationToken ct)
    {
        var results = await _dbContext.Outlet.ToListAsync();
        await SendAsync(results);
    }
    }
}