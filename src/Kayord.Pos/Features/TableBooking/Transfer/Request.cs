using FluentValidation;

namespace Kayord.Pos.Features.TableBooking.Transfer;

public class Request
{
    public int TableBookingId { get; set; }
    public string TransferUserId { get; set; } = string.Empty;
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.TableBookingId).GreaterThan(0).WithMessage("TableBookingId must be greater than 0");
        RuleFor(v => v.TransferUserId).NotEmpty().WithMessage("Transfer User is required");
    }
}