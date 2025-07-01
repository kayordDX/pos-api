using FluentValidation;

namespace Kayord.Pos.Features.User.LinkAccount;

public class Request
{
    public string OTP { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.OTP).Length(6).WithMessage("OTP Must have length of 6");
    }
}