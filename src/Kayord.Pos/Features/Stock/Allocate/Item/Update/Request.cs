namespace Kayord.Pos.Features.Stock.Allocate.Item.Update;

public class Request
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public decimal Actual { get; set; }
}