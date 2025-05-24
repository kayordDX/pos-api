using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Kayord.Pos.Features.Table.GetAvailable;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Menu.GetItem
{
    public class GetMenuItemsEndpoint : Endpoint<Request, MenuItemDTO>
    {
        private readonly AppDbContext _dbContext;

        public GetMenuItemsEndpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/menu/item");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            // var what = await _dbContext.Database.SqlQuery<MenuItemResponse>($"""
            //     select
            //         mi.menu_item_id,
            //         mi.menu_section_id,
            //         mi.name,
            //         mi.description,
            //         mi.price,
            //         mi.search_vector,
            //         mi."position",
            //         mi.division_id,
            //         mi.is_available,
            //         mi.stock_price,
            //         mi.is_enabled,
            //         mi.created,
            //         mi.created_by,
            //         mi.last_modified,
            //         mi.last_modified_by,
            //         t.tag_id,
            //         t.name tag_name
            //     from menu_item mi
            //     left join tag t
            //         on t.menu_item_id = mi.menu_item_id
            //     where mi.menu_item_id = 1
            // """).ToListAsync(ct);

            // var what2 = what.GroupBy(x => x.MenuItemId)
            //     .Select((g, m) => new MenuItemDTO
            //     {
            //         MenuItemId = g.Key,
            //         Description = g.First().Description,
            //         Tags = [.. g.Select(t => new Tag { Name = t.Name, TagId = t.TagId })]
            //     }).ToList();


            var result = await _dbContext.MenuItem
                .Where(x => x.MenuItemId.Equals(req.Id))
                .ProjectToDto()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }
            await SendAsync(result);
        }
    }
}
