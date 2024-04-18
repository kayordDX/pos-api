using Kayord.Pos.Entities;

namespace Kayord.Pos.Entities;

public class OutletExtraGroup
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
    public int ExtraGroupId { get; set; }
    public ExtraGroup ExtraGroup { get; set; } = default!;

}