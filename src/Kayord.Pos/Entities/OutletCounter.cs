namespace Kayord.Pos.Entities;

public class OutletCounter : AuditableEntity
{
    public Guid Id { get; set; }
    public string DeviceName { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
}