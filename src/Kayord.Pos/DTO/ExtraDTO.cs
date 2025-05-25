namespace Kayord.Pos.DTO;

public class ExtraDTO
{
    public int ExtraId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public decimal Price { get; set; }
    public int ExtraGroupId { get; set; }
    public bool IsAvailable { get; set; }
    public ExtraGroupBasicDTO ExtraGroup { get; set; } = default!;
}
