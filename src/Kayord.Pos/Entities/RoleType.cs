namespace Kayord.Pos.Entities;

public class RoleType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool isFrontLine { get; set; } = false;
    public bool isBackOffice { get; set; } = false;
    public string Description { get; set; } = string.Empty;
}