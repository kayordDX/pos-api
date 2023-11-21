using FluentValidation;

namespace Kayord.Pos.Features.Session.Login;

public class Request
{
    public int StaffId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.StaffId).NotEmpty().WithMessage("StaffId is required");
    }
}