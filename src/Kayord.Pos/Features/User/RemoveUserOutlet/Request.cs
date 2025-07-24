using FluentValidation;

namespace Kayord.Pos.Features.User.RemoveUserOutlet;

public class Request
{
    public string UserId { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.UserId).MinimumLength(1).WithMessage("UserId is required");
    }
}