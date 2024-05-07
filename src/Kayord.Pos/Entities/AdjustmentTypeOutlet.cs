namespace Kayord.Pos.Entities;

public class AdjustmentTypeOutlet
{
    public int Id { get; set; }
    public int AdjustmentTypeId { get; set; }
    public AdjustmentType AdjustmentType { get; set; } = default!;
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
}