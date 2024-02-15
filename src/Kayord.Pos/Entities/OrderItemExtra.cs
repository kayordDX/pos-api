namespace Kayord.Pos.Entities;
public class OrderItemExtra
{
    public int OrderItemExtraId { get; set; }
    public int OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; } = default!;
    public int ExtraId { get; set; }
    public Extra Extra { get; set; } = default!;

}