namespace Kayord.Pos.Entities;
public class OrderItem
{
    public int OrderItemId { get; set; }
    public int TableBookingId { get; set; }
    public TableBooking TableBooking { get; set; } = default!;
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
    public DateTime OrderReceived { get; set; } = DateTime.Now;
    public DateTime? OrderCompleted { get; set; }
    public int OrderItemStatusId { get; set; }
    public List<Option>? Options { get; set; }
    public List<Extra>? Extras { get; set; }
}