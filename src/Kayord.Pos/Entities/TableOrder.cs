namespace Kayord.Pos.Entities;
public class TableOrder
{
    public int TableOrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    public int TableBookingId { get; set; }
    public int? OrderItemId { get; set; }
    public OrderItem? OrderItem { get; set; }


}