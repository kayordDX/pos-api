using FluentValidation;

namespace Kayord.Pos.Features.Pay.ManualPayment;

public class Request
{
    public int PaymentTypeId { get; set; }
    public int TableBookingId { get; set; }
    public decimal Amount { get; set; }

}

