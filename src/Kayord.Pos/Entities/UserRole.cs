namespace Kayord.Pos.Entities;


public class UserRole
{
    public int UserRoleId { get; set; }
    public string UserId { get; set; }
    public int RoleId { get; set; }

    public User User { get; set; }
    public Role Role { get; set; }
}