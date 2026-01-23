using Kayord.Pos.Common.Models;

namespace Kayord.Pos.Features.TableBooking.HistoryAll;

public class Request : QueryModel
{
    public int TableBookingId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
