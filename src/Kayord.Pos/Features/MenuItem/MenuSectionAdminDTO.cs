namespace Kayord.Pos.Features.MenuItem;
public class MenuSectionAdminDTO
{
    public int MenuSectionId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int MenuId { get; set; }
    public MenuAdminDTO Menu { get; set; } = default!;
}