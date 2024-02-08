using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.DTO;
using Kayord.Pos.Features.Table.GetAvailable;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Kayord.Pos.Features.Menu.GetItems
{
    public class GetOutletMenusEndpoint : Endpoint<Request, List<MenuItemDTO>>
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
            Get("/menu/items");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
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

            if (!string.IsNullOrEmpty(req.Search))
            {
                items = items.Where(p => p.SearchVector.Matches(EF.Functions.ToTsQuery($"{req.Search}:*")));
            }


            var response = await items
                .Include(m => m.Tags)
                .Include(m => m.Extras)
                .ProjectToDto()
                .ToListAsync();
            await SendAsync(response);
        }
    }
}
