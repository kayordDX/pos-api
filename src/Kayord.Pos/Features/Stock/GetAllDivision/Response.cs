namespace Kayord.Pos.Features.Stock.GetAllDivision;

public class Response
{
    public int StockItemId { get; set; }
    public int StockId { get; set; }
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public string UnitName { get; set; } = default!;
    public int StockCategoryId { get; set; }
    public string? CategoryDisplayName { get; set; }
    public decimal Actual { get; set; }
    public decimal Threshold { get; set; }
    public bool HasVat { get; set; }
    public int DivisionId { get; set; }
    public DateTime Updated { get; set; }
}