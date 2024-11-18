

namespace Kayord.Pos.Features.Menu.Sections.Create;

public class Request
{
    public string Name { get; set; } = string.Empty;
    public int MenuId { get; set; }
    public int? PositionId { get; set; }

}

