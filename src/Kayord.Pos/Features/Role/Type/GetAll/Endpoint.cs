using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Role.Type.GetAll
{
    public class Endpoint : Endpoint<Request, List<Entities.RoleType>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/role/type/{outletId}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var roles = await _dbContext.RoleType.ToListAsync();
            await SendAsync(roles);
        }
    }
}