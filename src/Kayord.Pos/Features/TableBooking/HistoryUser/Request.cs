using Kayord.Pos.Common.Models;

namespace Kayord.Pos.Features.TableBooking.HistoryUser;

public class Request : QueryModel
{
    public string UserId { get; set; } = string.Empty;
    public int CashUpUserId { get; set; }
    public int TableBookingId { get; set; }
    public int OutletId { get; set; }
}
