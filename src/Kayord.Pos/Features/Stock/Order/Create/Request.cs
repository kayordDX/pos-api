namespace Kayord.Pos.Features.Stock.Order.Create;

public class Request
{
    public int OutletId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int StockLocationId { get; set; }
    public int SupplierId { get; set; }
}