namespace Kayord.Pos.Entities;

public class SalesPeriod
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Outlet Outlet { get; set; } = default!;
    public int OutletId { get; set; }
}
