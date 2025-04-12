namespace Kayord.Pos.Entities;

public class StockAllocateItem : AuditableEntity
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public decimal Actual { get; set; }
    public int StockAllocateItemStatusId { get; set; }
    public StockAllocateItemStatus StockAllocateItemStatus { get; set; } = default!;
    public DateTime Completed { get; set; }
    public int StockAllocateId { get; set; }
    public string AssignedUserId { get; set; } = string.Empty;
    public User? AssignedUser { get; set; }
    public StockAllocate StockAllocate { get; set; } = default!;
}