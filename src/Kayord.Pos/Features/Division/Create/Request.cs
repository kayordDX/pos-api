

namespace Kayord.Pos.Features.Division.Create;

public class Request
{
    public int OutletId { get; set; }
    public int DivisionTypeId { get; set; }
    public string Name { get; set; } = string.Empty;

}

