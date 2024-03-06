namespace Kayord.Pos.Features.Manager.OrderView;
public class MenuItemDTO
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Position { get; set; }
    public int? DivisionId { get; set; }
}