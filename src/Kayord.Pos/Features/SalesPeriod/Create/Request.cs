using FluentValidation;

namespace Kayord.Pos.Features.SalesPeriod.Create;

public class Request
{
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public int OutletId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
    }
}