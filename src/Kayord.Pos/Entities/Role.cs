namespace Kayord.Pos.Entities;


public class Role : AuditableEntity
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int RoleTypeId { get; set; }
    public RoleType RoleType { get; set; } = default!;
    public int? OutletId { get; set; }
}
