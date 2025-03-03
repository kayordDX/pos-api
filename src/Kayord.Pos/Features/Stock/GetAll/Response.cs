namespace Kayord.Pos.Features.Stock.GetAll;

public class Response
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public string UnitName { get; set; } = default!;
    public int StockCategoryId { get; set; }
    public decimal TotalActual { get; set; }
}