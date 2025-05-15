namespace Kayord.Pos.Entities;

public class StockCategory : AuditableEntity
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
}