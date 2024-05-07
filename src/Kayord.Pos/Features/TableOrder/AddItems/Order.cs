namespace Kayord.Pos.Features.Order.AddItems;

public class Order
{
    public int MenuItemId { get; set; } = default!;
    public List<int>? OptionIds { get; set; }
    public List<int>? ExtraIds { get; set; }
    public string Note { get; set; } = string.Empty;
    public int Quantity { get; set; }
}