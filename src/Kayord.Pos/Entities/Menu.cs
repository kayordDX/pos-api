namespace Kayord.Pos.Entities;
public class Menu
{
    public int MenuId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public  Outlet Outlet { get; set; } = default!;
    public  ICollection<MenuItem> MenuItems { get; set; } = default!;
}