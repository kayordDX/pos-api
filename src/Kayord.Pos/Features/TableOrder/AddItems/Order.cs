using FluentValidation;

namespace Kayord.Pos.Features.Order.AddItems
{
    public class Order
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; } = default!;
        public List<int>? OptionIds { get; set; }
        public List<int>? ExtraIds { get; set; }
    }


}