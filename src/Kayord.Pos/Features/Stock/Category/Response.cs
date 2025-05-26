namespace Kayord.Pos.Features.Stock.Category;

public class Response
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
}