namespace Kayord.Pos.Features.Stock.Items.Get;

public class Response
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public string StockName { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public int DivisionId { get; set; }
    public string DivisionName { get; set; } = string.Empty;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
}