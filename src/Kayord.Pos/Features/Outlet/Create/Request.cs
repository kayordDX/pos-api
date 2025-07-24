using FluentValidation;

namespace Kayord.Pos.Features.Outlet.Create;

public class Request
{
    public string Name { get; set; } = string.Empty;
    public int BusinessId { get; set; }

}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
    }
}