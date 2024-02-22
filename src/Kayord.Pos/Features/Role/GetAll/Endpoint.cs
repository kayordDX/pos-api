using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Role.GetAll
{
    public class Endpoint : EndpointWithoutRequest<List<Pos.Entities.Role>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/role");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var roles = await _dbContext.Role.ToListAsync();
            await SendAsync(roles);
        }
    }
}