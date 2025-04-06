namespace Kayord.Pos.Features.Stock.Allocate.Create;

public class Request
{
    public int OutletId { get; set; }
    public int ToOutletId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int FromDivisionId { get; set; }
    public int ToDivisionId { get; set; }
    public string AssignedUserId { get; set; } = string.Empty;
}