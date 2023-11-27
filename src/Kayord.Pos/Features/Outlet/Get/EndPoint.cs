using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Outlet.Get
{
    public class Endpoint : Endpoint<int, Pos.Entities.Outlet>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/outlet/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(int id, CancellationToken ct)
        {
            var entity = await _dbContext.Outlet.FindAsync(id);
            if (entity == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(entity);
        }
    }
}