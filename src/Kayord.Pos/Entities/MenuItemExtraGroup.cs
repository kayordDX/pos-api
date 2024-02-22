namespace Kayord.Pos.Entities;
public class MenuItemExtraGroup
{
    public int MenuItemId { get; set; }
    public int ExtraGroupId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
    public ExtraGroup ExtraGroup { get; set; } = default!;

}