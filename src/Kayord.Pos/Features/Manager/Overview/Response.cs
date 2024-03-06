using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.Manager.OrderView;

public class Response
{
    public int DivisionId { get; set; }
    public Division Division { get; set; } = default!;
    public List<TableBookingDTO>? Tables { get; set; }
    public DateTime LastRefresh { get; set; }
    public int PendingTables { get; set; }
    public int PendingItems { get; set; }
}