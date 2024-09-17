namespace Kayord.Pos.DTO;

public class PaymentTypeDTO
{
    public int PaymentTypeId { get; set; }
    public string PaymentTypeName { get; set; } = string.Empty;
    public decimal TipLevyPercentage { get; set; }
    public decimal DiscountPercentage { get; set; }
    public ICollection<OutletPaymentTypeDTO>? OutletPaymentTypes { get; set; }
    public bool CanEdit { get; set; }
}