using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Menu.Update
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
            Put("/menu/{menuId:int}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var entity = await _dbContext.Menu.FindAsync(req.Id);
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