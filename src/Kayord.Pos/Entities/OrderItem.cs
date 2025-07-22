namespace Kayord.Pos.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int? OrderGroupId { get; set; }
    public OrderGroup? OrderGroup { get; set; }
    public int TableBookingId { get; set; }
    public TableBooking TableBooking { get; set; } = default!;
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
    public DateTime OrderReceived { get; set; } = DateTime.UtcNow;
    public DateTime OrderUpdated { get; set; } = DateTime.Now;
    public DateTime? OrderCompleted { get; set; }
    public int OrderItemStatusId { get; set; }
    public OrderItemStatus OrderItemStatus { get; set; } = default!;
    public List<OrderItemOption>? OrderItemOptions { get; set; }
    public List<OrderItemExtra>? OrderItemExtras { get; set; }
    public string? Note { get; set; }
    public string? UserId { get; set; }
}