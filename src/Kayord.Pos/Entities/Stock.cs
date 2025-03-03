namespace Kayord.Pos.Entities;

public class Stock : AuditableEntity
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = default!;
    public int StockCategoryId { get; set; }
    public StockCategory StockCategory { get; set; } = default!;
    public List<StockItem>? StockItems { get; set; }
}