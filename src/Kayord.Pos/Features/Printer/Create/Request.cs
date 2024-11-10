using FluentValidation;

namespace Kayord.Pos.Features.Printer.Create;

public class Request
{
    public int OutletId { get; set; }
    public int DeviceId { get; set; }
    public string PrinterName { get; set; } = string.Empty;
    public string IPAddress { get; set; } = "10.0.0.3";
    public int Port { get; set; } = 9100;
    public int LineCharacters { get; set; } = 64;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.OutletId).NotEmpty().WithMessage("OutletId is required");
    }
}