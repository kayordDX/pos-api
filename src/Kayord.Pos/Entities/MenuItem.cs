namespace Kayord.Pos.Entities;
public class MenuItem
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int MenuId { get; set; }
    public Menu Menu { get; set; } = default!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = default!;
}