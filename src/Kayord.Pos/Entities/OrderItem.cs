namespace Kayord.Pos.Entities;
public class OrderItem
{
    public int OrderItemId { get; set; }
    public int TableBookingId { get; set; } = default;
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
    public List<Option>? Options { get; set; }
    public List<Extra>? Extras { get; set; }
}