using FluentValidation;

namespace Kayord.Pos.Features.Printer.ScanResults;

public class Request
{
    public int OutletId { get; set; }
    public int DeviceId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.OutletId).NotEmpty().WithMessage("OutletId is required");
        RuleFor(v => v.DeviceId).NotEmpty().WithMessage("DeviceId is required");
    }
}