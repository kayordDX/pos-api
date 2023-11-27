namespace Kayord.Pos.Entities;
public class OrderItem
{
    public int OrderItemId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = default!;
    public virtual MenuItem MenuItem { get; set; }= default!;
}