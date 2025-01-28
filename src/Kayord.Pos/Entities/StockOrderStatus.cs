namespace Kayord.Pos.Entities;

public class StockOrderStatus : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}