

namespace Kayord.Pos.Features.MenuItem.Update;

public class Request
{
    public int Id { get; set; }
    public int MenuSectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PositionId { get; set; }
    public int? DivisionId { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsEnabled { get; set; } = true;
    public List<int>? ExtraGroupIds { get; set; }

}

