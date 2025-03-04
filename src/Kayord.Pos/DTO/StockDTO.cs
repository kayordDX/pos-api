namespace Kayord.Pos.DTO;

public class StockDTO
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public UnitDTO Unit { get; set; } = default!;
    public int StockCategoryId { get; set; }
    public List<StockItemDTO>? StockItems { get; set; }
    public bool HasVat { get; set; }
}