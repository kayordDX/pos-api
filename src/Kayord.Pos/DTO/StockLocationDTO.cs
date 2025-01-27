namespace Kayord.Pos.DTO;

public class StockLocationDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AddressId { get; set; }
    public int OutletId { get; set; }
}