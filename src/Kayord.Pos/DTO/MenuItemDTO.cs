using Riok.Mapperly.Abstractions;

namespace Kayord.Pos.DTO;

public class MenuItemDTO
{
    public int MenuItemId { get; set; }
    public int MenuSectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Position { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<Extra>? Extras { get; set; }
    public Common.Enums.Division Division { get; set; }
    public ICollection<MenuItemOptionGroupDTO>? MenuItemOptionGroups { get; set; }
}

