namespace Kayord.Pos.Features.TableBooking.PaymentEdit;
public class Request
{
    public int PaymentId { get; set; }
    public int PaymentTypeId { get; set; }
    public decimal Amount { get; set; }
}