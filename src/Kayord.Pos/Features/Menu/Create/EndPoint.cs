using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;


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

            var menuSection = await _dbContext.MenuSection.FirstOrDefaultAsync(x => x.MenuSectionId == req.MenuSectionId);
            var menuEntity = new Pos.Entities.Menu
            {
                OutletId = req.OutletId,
                Name = req.Name,
                MenuSection = menuSection
            };
 
            await _dbContext.Menu.AddAsync(menuEntity);
            await _dbContext.SaveChangesAsync();

        }
    }
}