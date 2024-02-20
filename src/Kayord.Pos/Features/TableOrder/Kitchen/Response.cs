namespace Kayord.Pos.Features.TableOrder.Kitchen;

public class Response
{
    public int TableBookingId { get; set; }
    public string TableName { get; set; } = string.Empty;
    public List<BillOrderItemDTO> OrderItems { get; set; } = new List<BillOrderItemDTO>();
}