namespace Kayord.Pos.Entities;
public class RoleDivision
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int? DivisionId { get; set; }
    public Division? Division { get; set; }

}