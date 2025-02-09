namespace Kayord.Pos.Entities;

public class StockOrderItemStatus : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}