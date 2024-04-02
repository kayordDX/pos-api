using Kayord.Pos.DTO;
using Kayord.Pos.Features.Manager.OrderView;

namespace Kayord.Pos.Features.TableBooking.GetHistory;

public class Response
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public TableDTO Table { get; set; } = default!;
    public string BookingName { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public DateTime? CloseDate { get; set; }
    public int SalesPeriodId { get; set; }
}