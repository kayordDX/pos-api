using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Menu.GetOutletMenu;

public class GetOutletMenusEndpoint : Endpoint<Request, List<Kayord.Pos.Entities.Menu>>
{
    private readonly AppDbContext _dbContext;

    public GetOutletMenusEndpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/menu/outletMenus");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var menus = await _dbContext.Menu
            .Where(m => m.OutletId == req.OutletId)
            .Include(m => m.MenuSections!)
                .ThenInclude(s => s.MenuItems!)
            // .ThenInclude(mi => mi.Options)
            .Include(m => m.MenuSections!)
                .ThenInclude(s => s.MenuItems!)
                    .ThenInclude(mi => mi.Tags)
            .Include(m => m.MenuSections!)
                .ThenInclude(s => s.MenuItems!)

            .Include(m => m.MenuSections!)
                .ThenInclude(s => s.SubMenuSections!)
                    .ThenInclude(ss => ss.MenuItems!)
            //   .ThenInclude(mi => mi.Options)
            .Include(m => m.MenuSections!)
                .ThenInclude(s => s.SubMenuSections!)
                    .ThenInclude(ss => ss.MenuItems!)
                        .ThenInclude(mi => mi.Tags)
            .Include(m => m.MenuSections!)
                .ThenInclude(s => s.SubMenuSections!)
                    .ThenInclude(ss => ss.MenuItems!)

            .Include(m => m.MenuSections!)
                .ThenInclude(s => s.SubMenuSections!)
                    .ThenInclude(ss => ss.MenuItems)
             .ToListAsync();

        await Send.OkAsync(menus);
    }
}
