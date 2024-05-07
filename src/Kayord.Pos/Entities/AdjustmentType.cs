namespace Kayord.Pos.Entities;

public class AdjustmentType
{
    public int AdjustmentTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}