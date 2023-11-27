using FluentValidation;

namespace Kayord.Pos.Features.Menu.CreateMenuItem
{
    public class Request
    {
        public int MenuId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.MenuId).GreaterThan(0).WithMessage("Menu Id must be greater than 0");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(v => v.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

        }
    }
}