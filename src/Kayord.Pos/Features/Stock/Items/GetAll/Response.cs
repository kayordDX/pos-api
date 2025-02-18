namespace Kayord.Pos.Features.Stock.Items.GetAll;

public class Response
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public string StockName { get; set; } = string.Empty;
    public int DivisionId { get; set; }
    public string DivisionName { get; set; } = string.Empty;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
}