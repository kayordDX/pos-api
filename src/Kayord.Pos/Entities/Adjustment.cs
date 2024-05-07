namespace Kayord.Pos.Entities;

public class Adjustment
{
    public int AdjustmentId { get; set; }
    public int AdjustmentTypeId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public decimal Amount { get; set; }
    public string? Note { get; set; }
}