namespace Kayord.Pos.Features.Stock.OrderItem.Update;

public class Request
{
    public int StockOrderId { get; set; }
    public int StockId { get; set; }
    public decimal OrderAmount { get; set; }
    public decimal Actual { get; set; }
    public decimal Price { get; set; }
    public int StockOrderItemStatusId { get; set; }
}