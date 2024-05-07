namespace Kayord.Pos.Entities;

public class Adjustment
{
    public int AdjustmentId { get; set; }
    public AdjustmentType AdjustmentType { get; set; } = default!;
    public int AdjustmentTypeId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public string UserId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Note { get; set; }
}