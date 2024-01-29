namespace Kayord.Pos.Entities;
public class Menu
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public  Outlet Outlet { get; set; } = default!;
    public  MenuSection? MenuSection { get; set; }  = default!;
    public  int MenuSectionId { get; set; }

}