namespace Kayord.Pos.Features.Kitchen.GetOrders;
public class TableBookingDTO
{
    public int TableBookingId { get; set; }
    public int TableId {get;set;} = default!;
    public TableDTO Table {get;set;} = default!;

    public List<BillOrderItemDTO>? TableOrders { get; set; }
}