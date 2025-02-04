using Kayord.Pos.DTO;

namespace Kayord.Pos.DTO;

public class StockOrderItemDTO
{
    public int StockOrderId { get; set; }
    public int StockId { get; set; }
    public StockBasicDTO Stock { get; set; } = default!;
    public decimal Actual { get; set; }
    public decimal Price { get; set; }
}