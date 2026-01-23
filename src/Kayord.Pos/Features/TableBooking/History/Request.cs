using Kayord.Pos.Common.Models;

namespace Kayord.Pos.Features.TableBooking.History;

public class Request : QueryModel
{
    public int TableBookingId { get; set; }
}
