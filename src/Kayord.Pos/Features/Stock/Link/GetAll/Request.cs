using FluentValidation;

namespace Kayord.Pos.Features.Stock.Link.GetAll;

public class Request
{
    public int Id { get; set; }
    public int LinkType { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id is required");
    }
}