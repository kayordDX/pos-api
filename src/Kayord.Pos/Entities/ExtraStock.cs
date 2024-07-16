namespace Kayord.Pos.Entities;

public class ExtraStock
{
    public int ExtraId { get; set; }
    public Extra Extra { get; set; } = default!;
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = default!;
    public decimal Quantity { get; set; }
}