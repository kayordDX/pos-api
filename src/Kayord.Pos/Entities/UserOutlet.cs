namespace Kayord.Pos.Entities;

public class UserOutlet
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
    public string UserId { get; set; } = string.Empty;
    public bool IsCurrent { get; set; }

}