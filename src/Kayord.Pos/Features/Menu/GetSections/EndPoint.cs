using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Menu.GetSections
{
    public class GetMenusSectionsEndpoint : Endpoint<Request, List<MenuSection>>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<GetMenusSectionsEndpoint> _logger;

        public GetMenusSectionsEndpoint(AppDbContext dbContext, ILogger<GetMenusSectionsEndpoint> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public override void Configure()
        {
            Get("/menu/sections");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            int? parentId = null;
            if (req.SectionId > 0)
            {
                parentId = req.SectionId;
            }

            var sections = _dbContext.MenuSection.Where(x => x.MenuId == req.MenuId && x.ParentId == parentId);
            var response = await sections.ToListAsync();
            await SendAsync(response);
        }
    }
}
