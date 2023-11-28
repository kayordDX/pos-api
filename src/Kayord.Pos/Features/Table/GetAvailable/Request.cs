using FluentValidation;

namespace Kayord.Pos.Features.Table.GetAvailable
{
    public class Request
    {
        public int OutletId { get; set; }
    }

    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(v => v.OutletId).NotEmpty().WithMessage("OutletId is required");
        }
    }
}