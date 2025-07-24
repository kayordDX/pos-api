using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
namespace Kayord.Pos.Features.Menu.GetItem;

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
        var menuItems = await _dbContext.Database.SqlQuery<MenuItemResponse>($"""
            select
                mi.menu_item_id,
                mi.menu_section_id,
                mi.name,
                mi.description,
                mi.price,
                mi."position",
                mi.division_id,                   
                t.tag_id,
                t.name tag_name
            from menu_item mi
            left join tag t
                on t.menu_item_id = mi.menu_item_id
            where mi.menu_item_id = {req.MenuItemId}
        """).ToListAsync(ct);

        var options = await _dbContext.Database.SqlQuery<OptionsResponse>($"""
            select
                miop.menu_item_id,
                miop.option_group_id,
                og.name option_group_name,
                og.min_selections,
                og.max_selections,
                o.option_id,
                o.name option_name,
                o.price,
                o.position_id,
                coalesce(min(((si.actual - os.quantity) >= 0)::int)::bool, true) is_available
            from menu_item_option_group miop
            join option_group og
                on og.option_group_id = miop.option_group_id
            join option o
                on o.option_group_id = og.option_group_id
            left join option_stock os
                on os.option_id = o.option_id
            left join stock_item si
                on si.stock_id = os.stock_id
                and si.division_id = {req.DivisionId}
            where miop.menu_item_id = {req.MenuItemId}
            group by
                miop.menu_item_id,
                miop.option_group_id,
                og.name,
                og.min_selections,
                og.max_selections,
                o.option_id,
                o.name,
                o.price,
                o.position_id
        """).ToListAsync(ct);

        var extras = await _dbContext.Database.SqlQuery<ExtrasResponse>($"""
            select
                eop.menu_item_id,
                eop.extra_group_id,
                eg.name extra_group_name,
                e.extra_id,
                e.name extra_name,
                e.price,
                e.position_id,
                coalesce(min(((si.actual - es.quantity) >= 0)::int)::bool, true) is_available
            from menu_item_extra_group eop
            join extra_group eg
            on eg.extra_group_id = eop.extra_group_id
            join extra e
            on e.extra_group_id = eg.extra_group_id
            left join extra_stock es
            on es.extra_id = e.extra_id
            left join stock_item si
            on si.stock_id = es.stock_id
            and si.division_id = {req.DivisionId}
            where eop.menu_item_id = {req.MenuItemId}
            group by
                eop.menu_item_id,
                eop.extra_group_id,
                eg.name,
                e.extra_id,
                e.name,
                e.price,
                e.position_id
        """).ToListAsync(ct); ;

        var optionsGrouped = options.GroupBy(x => x.OptionGroupId)
            .Select((g, m) => new MenuItemOptionGroupDTO
            {
                OptionGroupId = g.Key,
                MenuItemId = g.First().MenuItemId,
                OptionGroup = new OptionGroupDTO
                {
                    OptionGroupId = g.Key,
                    MaxSelections = g.First().MaxSelections,
                    MinSelections = g.First().MinSelections,
                    Name = g.First().OptionGroupName,
                    Options = g.GroupBy(oo => oo.OptionId).Select((o, om) => new OptionDTO
                    {
                        OptionId = o.Key,
                        Name = o.First().OptionName,
                        Price = o.First().Price,
                        PositionId = o.First().PositionId,
                        IsAvailable = o.First().IsAvailable,
                        OptionGroupId = o.First().OptionGroupId,
                        OptionGroup = new OptionGroupBasicDTO
                        {
                            OptionGroupId = g.Key,
                            MaxSelections = g.First().MaxSelections,
                            MinSelections = g.First().MinSelections,
                            Name = g.First().OptionGroupName,
                        }
                    }).ToList()
                }
            }).ToList();

        var extrasGrouped = extras.GroupBy(x => x.ExtraGroupId)
            .Select((g, m) => new MenuItemExtraGroupDTO
            {
                ExtraGroupId = g.Key,
                MenuItemId = g.First().MenuItemId,
                ExtraGroup = new ExtraGroupDTO
                {
                    ExtraGroupId = g.Key,
                    Name = g.First().ExtraGroupName,
                    Extras = g.GroupBy(oo => oo.ExtraId).Select((o, om) => new ExtraDTO
                    {
                        ExtraId = o.Key,
                        Name = o.First().ExtraName,
                        Price = o.First().Price,
                        PositionId = o.First().PositionId,
                        IsAvailable = o.First().IsAvailable,
                        ExtraGroupId = o.First().ExtraGroupId,
                        ExtraGroup = new ExtraGroupBasicDTO
                        {
                            ExtraGroupId = g.Key,
                            Name = g.First().ExtraGroupName,
                        }
                    }).ToList()
                }
            }).ToList();

        var result = menuItems.GroupBy(x => x.MenuItemId)
            .Select((g, m) => new MenuItemDTO
            {
                MenuItemId = g.Key,
                MenuSectionId = g.First().MenuSectionId,
                Name = g.First().Name,
                Description = g.First().Description,
                Price = g.First().Price,
                Position = g.First().Position,
                DivisionId = g.First().DivisionId,
                Tags = g.Where(t => t.TagId != null)
                          .Select(t => new Tag { Name = t.TagName ?? string.Empty, TagId = t.TagId ?? 0 })
                          .ToList(),
                // Tags = [.. g.Select(t => new Tag { Name = t.Name, TagId = t.TagId ?? 0 })],
                MenuItemOptionGroups = optionsGrouped,
                MenuItemExtraGroups = extrasGrouped,
            }).FirstOrDefault();

        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }
        await SendAsync(result);
    }
}
