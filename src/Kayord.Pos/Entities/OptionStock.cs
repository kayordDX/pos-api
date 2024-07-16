namespace Kayord.Pos.Entities;

public class OptionStock
{
    public int OptionId { get; set; }
    public Option Option { get; set; } = default!;
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = default!;
    public decimal Quantity { get; set; }
}