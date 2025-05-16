namespace Kayord.Pos.DTO;

public class StockOrderItemDTO
{
    public int StockOrderId { get; set; }
    public int StockId { get; set; }
    public StockBasicDTO Stock { get; set; } = default!;
    public decimal OrderAmount { get; set; }
    public int StockOrderItemStatusId { get; set; }
    public StockOrderItemStatusDTO StockOrderItemStatus { get; set; } = default!;
    public decimal Actual { get; set; }
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}