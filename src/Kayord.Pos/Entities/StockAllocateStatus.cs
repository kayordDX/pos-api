namespace Kayord.Pos.Entities;

public class StockAllocateStatus : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}