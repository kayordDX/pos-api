namespace Kayord.Pos.Features.TableOrder.Office.OrderBased.Back;

public class Response
{
    public List<OrderGroupDTO>? OrderGroups { get; set; }
    public DateTime LastRefresh { get; set; }
    public int PendingOrders { get; set; }
    public int PendingItems { get; set; }
}