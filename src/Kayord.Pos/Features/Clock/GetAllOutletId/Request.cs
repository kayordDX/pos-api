using FluentValidation;

namespace Kayord.Pos.Features.Clock.List;

public class Request
{
    public int OutletId { get; set; }
    public int StatusId { get; set; }

}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.OutletId).GreaterThan(0).WithMessage("OutletId must be greater than 0");
    }
}