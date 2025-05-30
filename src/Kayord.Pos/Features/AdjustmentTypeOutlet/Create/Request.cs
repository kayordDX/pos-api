namespace Kayord.Pos.Features.AdjutmentTypeOutlet.Create;

public class Request
{
    public int OutletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

