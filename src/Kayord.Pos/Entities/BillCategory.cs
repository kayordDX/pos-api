namespace Kayord.Pos.Entities;

public class BillCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OutletId { get; set; }
}
