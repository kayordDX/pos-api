namespace Kayord.Pos.Features.Pay.HaloPay;

public class Request
{
    public decimal Amount { get; set; }
    public int TableBookingId { get; set; }
    public string UserId { get; set; } = string.Empty;
}