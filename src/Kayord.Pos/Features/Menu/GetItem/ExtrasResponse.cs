namespace Kayord.Pos.Features.Menu.GetItem;

public class ExtrasResponse
{
    public int MenuItemId { get; set; }
    public int ExtraGroupId { get; set; }
    public string ExtraGroupName { get; set; } = string.Empty;
    public int ExtraId { get; set; }
    public string ExtraName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PositionId { get; set; }
    public bool IsAvailable { get; set; }
}