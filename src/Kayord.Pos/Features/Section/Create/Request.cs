using FluentValidation;

namespace Kayord.Pos.Features.Section.Create;

public class Request
{
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }

}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
    }
}