namespace Kayord.Pos.Entities;

public class Stock
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public Location Location { get; set; } = default!;
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = default!;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
    public bool IsBulkRecipe { get; set; }
    public int StockCategoryId { get; set; }
    public StockCategory StockCategory { get; set; } = default!;
}