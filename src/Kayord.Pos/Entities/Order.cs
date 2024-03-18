namespace Kayord.Pos.Entities;
public class Order
{
    public int OrderId { get; set; }
    public int OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; } = default!;
}