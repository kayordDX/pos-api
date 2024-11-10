using FluentValidation;

namespace Kayord.Pos.Features.Printer.Edit;

public class Request
{
    public int Id { get; set; }
    public string PrinterName { get; set; } = string.Empty;
    public string IPAddress { get; set; } = "10.0.0.3";
    public int Port { get; set; } = 9100;
    public int LineCharacters { get; set; } = 64;
    public bool IsEnabled { get; set; }
    public int DeviceId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.Id).NotEmpty().WithMessage("Id is required");
    }
}