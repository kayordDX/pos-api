namespace Kayord.Pos.Entities;

public class StockAllocateItem : AuditableEntity
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; } = default!;
    public int DivisionId { get; set; }
    public Division Division { get; set; } = default!;
    public decimal Threshold { get; set; }
    public decimal Actual { get; set; }
}