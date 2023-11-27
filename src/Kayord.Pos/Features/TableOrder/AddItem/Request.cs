using FluentValidation;

namespace Kayord.Pos.Features.Order.AddItem
{
    public class Request
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.OrderId).GreaterThan(0).WithMessage("OrderId must be greater than 0");
            RuleFor(v => v.MenuItemId).GreaterThan(0).WithMessage("MenuItemId must be greater than 0");

        }
    }
}