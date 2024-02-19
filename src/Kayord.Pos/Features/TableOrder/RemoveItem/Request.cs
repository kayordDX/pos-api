using FluentValidation;

namespace Kayord.Pos.Features.Order.RemoveItem
{
    public class Request
    {
        public int OrderItemId { get; set; } = default!;

    }


}