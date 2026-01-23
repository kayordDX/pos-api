using Kayord.Pos.Common.Models;

namespace Kayord.Pos.Features.TableBooking.PeriodHistory;

public class Request : QueryModel
{
    public int TableBookingId { get; set; }
    public int SalesPeriodId { get; set; }
}
