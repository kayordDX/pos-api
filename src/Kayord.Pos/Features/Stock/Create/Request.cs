namespace Kayord.Pos.Features.Stock.Create;

public class Request
{
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public bool HasVat { get; set; }
    public int StockCategoryId { get; set; }
}