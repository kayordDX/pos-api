using Humanizer;
using Kayord.Pos.DTO;


namespace Kayord.Pos.Features.Order.BackOffice;
public class TableBookingDTO
{
    public int Id { get; set; }
    public int TableId { get; set; } = default!;
    public TableDTO Table { get; set; } = default!;
    public List<OrderItemDTO>? OrderItems { get; set; }
    public string BookingName { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public DateTime? CloseDate { get; set; }
    public UserDTO User { get; set; } = default!;
}