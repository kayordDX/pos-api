using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Section.Create
{
    public class Endpoint : Endpoint<Request, Pos.Entities.Section>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/section");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            Pos.Entities.Section entity = new Pos.Entities.Section()
            {
                Name = req.Name,
                OutletId = req.OutletId
            };
            await _dbContext.Section.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Section.FindAsync(entity.Id);
            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(result);
        }
    }
}