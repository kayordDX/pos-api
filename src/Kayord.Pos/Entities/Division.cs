namespace Kayord.Pos.Entities;

public class Division
{
    public int DivisionId { get; set; }
    public string DivisionName { get; set; } = string.Empty;
    public string? FriendlyName { get; set; }
    public int OutletId { get; set; }
    public int DivisionTypeId { get; set; }
}