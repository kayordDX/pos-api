namespace Kayord.Pos.Entities;

public class UserRoleOutlet
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public int OutletId { get; set; }
    public User User { get; set; } = default!;
    public Outlet Outlet { get; set; } = default!;
    public Role? Role { get; set; }
}
