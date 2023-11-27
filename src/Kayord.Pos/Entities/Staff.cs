using Kayord.Pos.Common.Enums;

namespace Kayord.Pos.Entities;

public class Staff
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public StaffType StaffType { get; set; }
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
}