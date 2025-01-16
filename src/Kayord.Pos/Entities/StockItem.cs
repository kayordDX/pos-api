namespace Kayord.Pos.Entities;

public class StockItem
{
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public int StockLocationId { get; set; }
    public StockLocation StockLocation { get; set; } = default!;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
}