namespace Kayord.Pos.Entities;

public class StockPeriodSnapshot
{
    public int StockItemId { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public int DivisionId { get; set; }
    public Division Division { get; set; } = default!;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
    public DateTime Updated { get; set; }
    public int OutletId { get; set; }
    public int SalesPeriodId { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}
