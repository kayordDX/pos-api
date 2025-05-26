namespace Kayord.Pos.DTO;

public class MenuItemOptionGroupDTO
{
    public int MenuItemId { get; set; }
    public int OptionGroupId { get; set; }
    public OptionGroupDTO OptionGroup { get; set; } = default!;
}