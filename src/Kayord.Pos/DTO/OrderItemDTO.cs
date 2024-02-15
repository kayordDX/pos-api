namespace Kayord.Pos.DTO;
public class OrderItemDTO
{
    public int OrderItemId { get; set; }
    public int TableBookingId { get; set; }
    public TableBookingDTO TableBooking { get; set; } = default!;
    public int MenuItemId { get; set; }
    public DateTime OrderReceived { get; set; } = DateTime.Now;
    public DateTime? OrderCompleted { get; set; }
    public int OrderItemStatusId { get; set; }
    public List<OptionDTO>? Options { get; set; }
    public List<ExtraDTO>? Extras { get; set; }
}