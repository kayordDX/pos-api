namespace Kayord.Pos.Entities;

public class Section : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
    public ICollection<Table>? Tables { get; set; }
}