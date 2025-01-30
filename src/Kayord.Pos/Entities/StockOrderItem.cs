namespace Kayord.Pos.Entities;

public class StockOrderItem : AuditableEntity
{
    public int StockOrderId { get; set; }
    public StockOrder StockOrder { get; set; } = default!;
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public string OrderNumber { get; set; } = string.Empty;
    public decimal Actual { get; set; }
    public decimal Price { get; set; }
}