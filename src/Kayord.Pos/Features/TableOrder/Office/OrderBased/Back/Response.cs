using Kayord.Pos.Features.TableOrder.Office;

namespace Kayord.Pos.Features.Order.BackOffice;

public class Response
{
    public List<OrderGroupDTO>? OrderGroups { get; set; }
    public DateTime LastRefresh { get; set; }
    public int PendingOrders { get; set; }
    public int PendingItems { get; set; }
}