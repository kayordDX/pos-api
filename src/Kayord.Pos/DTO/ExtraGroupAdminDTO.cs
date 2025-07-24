namespace Kayord.Pos.DTO;

public class ExtraGroupAdminDTO
{
    public int ExtraGroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsGlobal { get; set; }
}