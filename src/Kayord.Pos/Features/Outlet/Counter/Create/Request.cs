using FluentValidation;

namespace Kayord.Pos.Features.Outlet.Counter.Create;

public class Request
{
    public string DeviceName { get; set; } = string.Empty;
    public int OutletId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.DeviceName).NotEmpty().WithMessage("DeviceName is required");
        RuleFor(v => v.OutletId).GreaterThan(0).WithMessage("OutletId is required");
    }
}