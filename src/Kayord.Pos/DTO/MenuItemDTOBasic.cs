namespace Kayord.Pos.DTO;
using Kayord.Pos.Entities;

public class MenuItemDTOBasic
{
    public int MenuItemId { get; set; }
    public int MenuSectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Position { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public MenuSectionBasicDTO MenuSection { get; set; } = default!;
}

