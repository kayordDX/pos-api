using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.MenuItem;

public class MenuItemAdminDTO
{
    public int MenuItemId { get; set; }
    public int MenuId { get; set; }
    public int MenuSectionId { get; set; }
    public MenuSectionAdminDTO MenuSection { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Position { get; set; }
    public int DivisionId { get; set; }
    public int? BillCategoryId { get; set; }

    public List<MenuItemOptionGroupDTO> MenuItemOptionGroups { get; set; } = default!;
    public List<MenuItemExtraGroupDTO> MenuItemExtraGroups { get; set; } = default!;
    public bool IsAvailable { get; set; }
    public bool IsEnabled { get; set; } = true;
    public decimal StockPrice { get; set; }
}