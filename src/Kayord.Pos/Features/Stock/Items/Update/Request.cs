namespace Kayord.Pos.Features.Stock.Items.Update;

public class Request
{
    public int DivisionId { get; set; }
    public int StockId { get; set; }
    public decimal Actual { get; set; }
    public decimal Threshold { get; set; }
}