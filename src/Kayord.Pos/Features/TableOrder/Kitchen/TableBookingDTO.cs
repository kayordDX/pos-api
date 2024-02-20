namespace Kayord.Pos.Features.TableOrder.Kitchen;
public class TableBookingDTO
{
    public int Id { get; set; }
    public int TableId { get; set; } = default!;
    public TableDTO Table { get; set; } = default!;
    public List<BillOrderItemDTO>? TableOrders { get; set; }
}