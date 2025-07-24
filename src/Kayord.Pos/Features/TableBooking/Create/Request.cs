using FluentValidation;

namespace Kayord.Pos.Features.TableBooking.Create;

public class Request
{
    public int TableId { get; set; }
    public string BookingName { get; set; } = string.Empty;
    public int SalesPeriodId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(v => v.TableId).GreaterThan(0).WithMessage("TableId must be greater than 0");
        RuleFor(v => v.BookingName).NotEmpty().WithMessage("BookingName is required");
        RuleFor(v => v.SalesPeriodId).GreaterThan(0).WithMessage("SalesPeriodId must be greater than 0");

    }
}