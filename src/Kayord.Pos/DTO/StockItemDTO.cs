namespace Kayord.Pos.Entities;

public class StockItemDTO
{
    public StockLocationDTO StockLocation { get; set; } = default!;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
}