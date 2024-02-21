namespace Kayord.Pos.Features.TableOrder.UpdateTableOrder
{
    public class Request
    {
        public int TableBookingId { get; set; } = default!;
        public int OrderItemStatusId { get; set; } = default!;

    }
}