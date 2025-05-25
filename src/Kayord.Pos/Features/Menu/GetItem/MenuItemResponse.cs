namespace Kayord.Pos.Features.Menu.GetItem;

public class MenuItemResponse
{
    public int MenuItemId { get; set; }
    public int MenuSectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Position { get; set; }
    public int DivisionId { get; set; }
    public int? TagId { get; set; }
    public string? TagName { get; set; }
}