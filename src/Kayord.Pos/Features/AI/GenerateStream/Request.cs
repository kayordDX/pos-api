using FluentValidation;

namespace Kayord.Pos.Features.AI.GenerateStream;

public class Request
{
    public string Prompt { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Prompt).NotEmpty().WithMessage("Prompt is required");
    }
}