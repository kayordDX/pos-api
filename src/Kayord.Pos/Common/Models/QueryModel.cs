namespace Kayord.Pos.Common.Models;

public class QueryModel
{
    public string? Sorts { get; set; }
    public string? Filters { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}