namespace Kayord.Pos.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public Location Location { get; set; } = default!;
    public int SupplierPlatformId { get; set; }
    public SupplierPlatform SupplierPlatform { get; set; } = default!;
}