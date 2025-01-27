namespace Kayord.Pos.DTO;

public class StockItemDTO
{
    public StockLocationDTO StockLocation { get; set; } = default!;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
}