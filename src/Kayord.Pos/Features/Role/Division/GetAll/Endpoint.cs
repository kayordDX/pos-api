using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Role.Division.GetAll
{
    public class Endpoint : Endpoint<Request, List<Entities.Role>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/role/division/{divisionid}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var roleids = await _dbContext.RoleDivision.Where(x => x.DivisionId == req.DivisionId).Select(x => x.RoleId).ToListAsync();

            var roles = await _dbContext.Role.Where(x => roleids.Contains(x.RoleId)).ToListAsync();

            await SendAsync(roles);
        }
    }
}