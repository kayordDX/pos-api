namespace Kayord.Pos.DTO;
using Kayord.Pos.Entities;
public class OrderItemDTO
{

    public int OrderItemId { get; set; }
    public int TableBookingId { get; set; } = default;
    public int MenuItemId { get; set; }
    public DateTime OrderReceived { get; set; } = DateTime.Now;
    public DateTime? OrderCompleted { get; set; }
    public int OrderItemStatusId { get; set; }





}