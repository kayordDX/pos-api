

namespace Kayord.Pos.Features.Menu.Sections.Update;

public class Request
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    //public int MenuId { get; set; }
    public int? PositionId { get; set; }

}

