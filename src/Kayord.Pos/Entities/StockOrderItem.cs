namespace Kayord.Pos.Entities;

public class StockOrderItem : AuditableEntity
{
    public int StockOrderId { get; set; }
    public StockOrder StockOrder { get; set; } = default!;
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public decimal OrderAmount { get; set; }
    public int StockOrderItemStatusId { get; set; }
    public StockOrderItemStatus StockOrderItemStatus { get; set; } = default!;
    public decimal Actual { get; set; }
    public decimal Price { get; set; }
}