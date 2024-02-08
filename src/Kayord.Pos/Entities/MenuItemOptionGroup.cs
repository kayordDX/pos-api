namespace Kayord.Pos.Entities;
public class MenuItemOptionGroup
{
    public int MenuItemId { get; set; }
    public int OptionGroupId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
    public OptionGroup OptionGroup { get; set; } = default!;

}