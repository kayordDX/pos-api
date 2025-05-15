namespace Kayord.Pos.Features.Extra.Create;

public class Request
{
    public string Name { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public decimal Price { get; set; }
    public int ExtraGroupId { get; set; }
    public int OutletId { get; set; }
    public bool IsAvailable { get; set; }
}

