using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.Menu.GetSections;

public class Response
{
    public List<MenuSectionDTO>? Sections { get; set; }
    public List<MenuSectionDTO>? Parents { get; set; }
}