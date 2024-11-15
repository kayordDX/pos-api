namespace Kayord.Pos.Entities;
public class Menu : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public int Position { get; set; }
    public Outlet Outlet { get; set; } = default!;
    public List<MenuSection>? MenuSections { get; set; }
}