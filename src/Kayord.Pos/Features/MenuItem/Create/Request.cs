

namespace Kayord.Pos.Features.MenuItem.Create;

public class Request
{
    public int MenuSectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PositionId { get; set; }
    public int? DivisionId { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsEnabled { get; set; } = true;
    public int? BillCategoryId { get; set; }

    public List<int>? ExtraGroupIds { get; set; }
    public List<int>? OptionGroupIds { get; set; }
}

