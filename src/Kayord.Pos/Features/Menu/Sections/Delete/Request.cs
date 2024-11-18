using FluentValidation;

namespace Kayord.Pos.Features.Menu.Sections.Delete;

public class Request
{
    public int Id { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id is required");
    }
}

