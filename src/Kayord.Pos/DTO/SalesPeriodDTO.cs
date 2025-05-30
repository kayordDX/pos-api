namespace Kayord.Pos.DTO;

public class SalesPeriodDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int OutletId { get; set; }
}