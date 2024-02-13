using FluentValidation;

namespace Kayord.Pos.Features.Order.AddItem
{
    public class Request
    {
        public List<Order> Orders { get; set; } = default!;
    }


}