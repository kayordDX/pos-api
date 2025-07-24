using FluentValidation;

namespace Kayord.Pos.Features.TableBooking.Get;

public class Request
{
    public int Id { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}