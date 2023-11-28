using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Table.GetAvailable
{
    public class Endpoint : Endpoint<Request, List<Pos.Entities.Table>>
    {
        private readonly AppDbContext _dbContext;
        private readonly CurrentUserService _user;

        public Endpoint(AppDbContext dbContext, CurrentUserService user)
        {
            _dbContext = dbContext;
            _user = user;
        }

        public override void Configure()
        {
            Get("/table/available");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var e = _user.Expires;
            var b = _user.Type;
            var results = await _dbContext.Table
                // .Include(x => x.Section)
                .Where(x => x.Section.OutletId == req.OutletId).ToListAsync();
            await SendAsync(results);
        }
    }
}