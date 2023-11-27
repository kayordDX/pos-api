using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Menu.Create
{
    public class Endpoint : Endpoint<Request, Pos.Entities.Menu>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/menu");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            Pos.Entities.Menu entity = new Pos.Entities.Menu()
            {
                OutletId = req.OutletId,
                Name = req.Name,
            };

            await _dbContext.Menu.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Menu.FindAsync(entity.Id);
            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(result);
        }
    }
}