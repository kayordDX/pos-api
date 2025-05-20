namespace Kayord.Pos.DTO;

public class StockOrderResponseDTO
{
    public int Id { get; set; }
    public int OutletId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int StockOrderStatusId { get; set; }
    public string StockOrderStatusName { get; set; } = default!;
    public int DivisionId { get; set; }
    public string DivisionName { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public DateTime Created { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = default!;
    public decimal Total { get; set; }
}