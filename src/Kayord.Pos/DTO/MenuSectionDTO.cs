namespace Kayord.Pos.DTO;
public class MenuSectionDTO
{
    public int MenuSectionId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int MenuId { get; set; }
    public int? ParentId { get; set; }
    public MenuSectionParentDTO? Parent { get; set; }
}