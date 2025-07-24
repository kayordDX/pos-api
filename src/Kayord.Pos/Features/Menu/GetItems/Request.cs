namespace Kayord.Pos.Features.Menu.GetItems;

public class Request
{
    public int MenuId { get; set; }
    public int SectionId { get; set; }
    public string? Search { get; set; }
}
