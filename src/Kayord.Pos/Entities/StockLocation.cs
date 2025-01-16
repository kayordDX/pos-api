namespace Kayord.Pos.Entities;

public class StockLocation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AddressId { get; set; }
    public Address Address { get; set; } = default!;
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;
}