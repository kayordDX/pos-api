namespace Kayord.Pos.Features.Stock.Allocate.Item.Create;

public class Request
{
    public int StockAllocateId { get; set; }
    public int StockId { get; set; }
    public decimal Actual { get; set; }
}