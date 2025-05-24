namespace Kayord.Pos.Features.Extra.GetAllMenu;

public class SpecialExtrasDTO
{
    public int ExtraId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public decimal Price { get; set; }
    public int ExtraGroupId { get; set; }
    public string ExtraGroupName { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}