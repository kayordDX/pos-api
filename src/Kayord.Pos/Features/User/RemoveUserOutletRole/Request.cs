using FluentValidation;

namespace Kayord.Pos.Features.User.RemoveUserOutletRole;

public class Request
{
    public string UserId { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.UserId).MinimumLength(1).WithMessage("UserId is required");
        RuleFor(v => v.Role).MinimumLength(1).WithMessage("Role is required");
    }
}