namespace Kayord.Pos.Entities;
public class MenuSection
{
    public int MenuSectionId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Menu Menu { get; set; } = default!;
    public int MenuId { get; set; }
    public MenuSection? Parent { get; set; }
    public int? ParentId { get; set; }

    public int? PositionId { get; set; }
    public List<MenuSection>? SubMenuSections { get; set; }
    public ICollection<MenuItem>? MenuItems { get; set; }
}