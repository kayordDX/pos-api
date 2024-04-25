namespace Kayord.Pos.Features.TableOrder.Office.OrderBased.Back;
public class OrderGroupDTO
{
    public int OrderGroupId { get; set; }
    public List<OrderItemDTO>? OrderItems { get; set; }
}