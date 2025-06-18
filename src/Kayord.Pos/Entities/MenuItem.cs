using NpgsqlTypes;

namespace Kayord.Pos.Entities;

public class MenuItem : AuditableEntity
{
    public int MenuItemId { get; set; }
    public MenuSection MenuSection { get; set; } = default!;
    public int MenuSectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? BillCategoryId { get; set; }
    public BillCategory? BillCategory { get; set; }
    public decimal Price { get; set; }
    public NpgsqlTsVector SearchVector { get; set; } = default!;
    public int Position { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public int? DivisionId { get; set; }
    public Division? Division { get; set; }
    public ICollection<MenuItemOptionGroup>? MenuItemOptionGroups { get; set; }
    public ICollection<MenuItemExtraGroup>? MenuItemExtraGroups { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsEnabled { get; set; } = true;
    public decimal StockPrice { get; set; }
}
