namespace Kayord.Pos.Features.Extra.Update;

public class Request
{

    public int ExtraId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public decimal Price { get; set; }
    public int ExtraGroupId { get; set; }
    public int OutletId { get; set; }

}

