using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Section.Update
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
            Put("/section/{sectionId:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var entity = await _dbContext.Section.FindAsync(req.Id);
            if (entity == null)
            {
                await SendNotFoundAsync();
                return;
            }

            entity.Name = req.Name;
          

            await _dbContext.SaveChangesAsync();
            await SendAsync(entity);
        }
    }
}