namespace Kayord.Pos.Entities;
public class MenuSection
{
    public int MenuSectionId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public MenuSection? Parent { get; set; }
    public int? ParentId { get; set; }
    public List<MenuSection>? SubMenuSections {get; set;}
    public ICollection<MenuItem> MenuItems { get; set; }
}