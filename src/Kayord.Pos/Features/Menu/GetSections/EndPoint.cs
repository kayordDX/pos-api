using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Menu.GetSections
{
    public class GetOutletMenusEndpoint : Endpoint<Request, Response>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<GetOutletMenusEndpoint> _logger;

        public GetOutletMenusEndpoint(AppDbContext dbContext, ILogger<GetOutletMenusEndpoint> logger)
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
            Response response = new();
            int? parentId = null;
            if (req.SectionId > 0)
            {
                parentId = req.SectionId;
            }

            var sections = _dbContext.MenuSection.Where(x => x.ParentId == parentId);
            var sectionsResult = await sections.ToListAsync();

            IQueryable<MenuItem>? items;
            if (req.SectionId == 0)
            {
                items = _dbContext.MenuItem;
            }
            else
            {
                var sectionParents = await _dbContext.Database.SqlQuery<MenuParents>($"""
                SELECT * FROM "getMenuSectionChildren"({req.SectionId})
                """).Select(s => s.Id).ToListAsync();

                items = _dbContext.MenuItem
                    .Where(e => sectionParents.Contains(e.MenuSectionId));
            }

            var itemsResult = await items.ToListAsync();

            response.Sections = sectionsResult;
            response.Items = itemsResult;

            await SendAsync(response);
        }
    }
}
