using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.Menu.GetSections;

public class Response
{
    public List<MenuSection> Sections { get; set; } = new List<MenuSection>();
    public List<MenuItem> Items { get; set; } = new List<MenuItem>();
}