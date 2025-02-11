namespace Kayord.Pos.Features.Stock.OrderItem.UpdateBulk;

public class Request
{
    public int StockOrderId { get; set; }
    public List<int>? StockIds { get; set; }
    public int StockOrderItemStatusId { get; set; }
}