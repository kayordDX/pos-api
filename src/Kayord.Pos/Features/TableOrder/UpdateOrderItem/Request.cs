namespace Kayord.Pos.Features.TableOrder.UpdateOrderItem
{
    public class Request
    {
        public List<int> OrderItemIds { get; set; } = default!;
        public int OrderItemStatusId { get; set; } = default!;
    }
}