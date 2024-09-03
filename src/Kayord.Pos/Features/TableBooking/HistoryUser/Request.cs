namespace Kayord.Pos.Features.TableBooking.HistoryUser;

public class Request
{
    public string UserId { get; set; } = string.Empty;
    public int CashUpUserId { get; set; }
    public int TableBookingId { get; set; }
}