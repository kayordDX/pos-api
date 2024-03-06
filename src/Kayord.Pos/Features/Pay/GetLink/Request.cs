namespace Kayord.Pos.Features.Pay.GetLink;

public class Request
{
    public decimal Amount { get; set; }
    public int TableBookingId { get; set; }

}