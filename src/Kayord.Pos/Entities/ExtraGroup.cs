namespace Kayord.Pos.Entities;

public class ExtraGroup
{
    public int ExtraGroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Extra> Extras { get; set; } = default!;
    public ICollection<MenuItemExtraGroup>? MenuItemExtraGroups { get; set; }
    public int OutletId { get; set; }
}