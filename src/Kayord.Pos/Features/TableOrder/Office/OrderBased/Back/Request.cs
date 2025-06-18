namespace Kayord.Pos.Features.TableOrder.Office.OrderBased.Back;

public class Request
{
    public string? DivisionIds { get; set; }
    public bool Complete { get; set; } = false;
}
