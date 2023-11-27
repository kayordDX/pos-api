using FluentValidation;

namespace Kayord.Pos.Features.Menu.Create
{
    public class Request
    {
        public int OutletId { get; set; }
        public string Name { get; set; } = string.Empty;
        
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.OutletId).GreaterThan(0).WithMessage("OutletId must be greater than 0");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");

        }
    }
}