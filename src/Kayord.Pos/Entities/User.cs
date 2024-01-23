namespace Kayord.Pos.Entities;

public class User
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }

    public bool IsActive { get; set; }

    public ICollection<UserRole>? UserRole { get; set; }
}