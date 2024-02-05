namespace Kayord.Pos.Entities;


public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<UserRole>? UserRole { get; set; }
}
