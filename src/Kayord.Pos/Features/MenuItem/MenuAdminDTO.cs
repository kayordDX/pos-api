namespace Kayord.Pos.Features.MenuItem;

public class MenuAdminDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public int Position { get; set; }
}