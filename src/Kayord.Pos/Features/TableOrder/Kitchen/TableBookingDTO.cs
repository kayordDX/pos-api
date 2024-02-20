namespace Kayord.Pos.Features.Kitchen.GetOrders;
public class TableBookingDTO
{
    public int TableBookingId { get; set; }
    public List<BillOrderItemDTO>? TableOrders { get; set; }
}