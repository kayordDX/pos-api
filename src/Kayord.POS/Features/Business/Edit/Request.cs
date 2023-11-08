using FluentValidation;

namespace Kayord.Pos.Features.Business.Edit;

public class Request
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
    }
}