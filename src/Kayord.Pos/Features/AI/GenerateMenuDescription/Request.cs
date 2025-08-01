using FluentValidation;

namespace Kayord.Pos.Features.AI.GenerateMenuDescription;

public class Request
{
    public string Menu { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Menu).NotEmpty().WithMessage("Menu is required");
        RuleFor(v => v.Section).NotEmpty().WithMessage("Section is required");
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
    }
}