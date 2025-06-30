namespace Kayord.Pos.Entities;

public class OutletFeature
{
    public int FeatureId { get; set; }
    public Feature Feature { get; set; } = default!;
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
}