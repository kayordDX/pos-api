using FluentValidation;

namespace Kayord.Pos.Features.Order.UpdateOrderItem
{
    public class Request
    {
        public int OrderItemId { get; set; } = default!;
        public int OrderItemStatusId { get; set; } = default!;
        public bool isComplete { get; set; } = false;


    }


}