using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.TableOrder.Office.OrderBased.Back;
public class OrderGroupDTO
{
    public int OrderGroupId { get; set; }
    public TableBookingDTO? TableBooking { get; set; }
    public int TableBookingId { get; set; }
    public List<OrderItemDTO>? OrderItems { get; set; }
}