namespace Kayord.Pos.Entities;

public class OptionGroup
{
    public int OptionGroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinSelections { get; set; }
    public int MaxSelections { get; set; }
    public ICollection<Option> Options { get; set; } = default!;
    public ICollection<MenuItemOptionGroup>? MenuItemOptionGroups { get; set; }
    public int OutletId { get; set; }
}