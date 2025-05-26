namespace Kayord.Pos.Features.Menu.GetItem;

public class OptionsResponse
{
    public int MenuItemId { get; set; }
    public int OptionGroupId { get; set; }
    public string OptionGroupName { get; set; } = string.Empty;
    public int MinSelections { get; set; }
    public int MaxSelections { get; set; }
    public int OptionId { get; set; }
    public string OptionName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PositionId { get; set; }
    public bool IsAvailable { get; set; }
}