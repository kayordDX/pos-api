namespace Kayord.Pos.Features.Stock.Link.GetAll;

public class Response
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal TotalActual { get; set; }

}