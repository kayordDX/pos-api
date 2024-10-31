using FluentValidation;

namespace Kayord.Pos.Features.Pay.PayConfig.SetActive;

public class Request
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public bool IsEnabled { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id is required");
    }
}
