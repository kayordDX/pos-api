using Kayord.Pos.Features.TableOrder.Office;

namespace Kayord.Pos.Features.TableOrder.FrontOffice;

public class Response
{
    public List<TableBookingDTO>? Tables { get; set; }
    public DateTime LastRefresh { get; set; }
    public int PendingTables { get; set; }
    public int PendingItems { get; set; }
}