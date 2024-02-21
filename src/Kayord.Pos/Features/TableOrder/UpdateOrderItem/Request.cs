namespace Kayord.Pos.Features.TableOrder.UpdateOrderItem
{
    public class Request
    {
        public int OrderItemId { get; set; } = default!;
        public int OrderItemStatusId { get; set; } = default!;
    }
}