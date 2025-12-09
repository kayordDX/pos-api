using FluentValidation;

namespace Kayord.Pos.Features.Bill.WhatsappBill;

public class Request
{
    public int TableBookingId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CountryCode { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.PhoneNumber).MinimumLength(5).WithMessage("Invalid phone number");
        RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required");
    }
}