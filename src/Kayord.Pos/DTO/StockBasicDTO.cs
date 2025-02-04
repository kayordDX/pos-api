namespace Kayord.Pos.DTO;

public class StockBasicDTO
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public UnitDTO Unit { get; set; } = default!;
    public int StockCategoryId { get; set; }
    public decimal TotalActual { get; set; }
}