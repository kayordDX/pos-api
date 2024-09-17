namespace Kayord.Pos.Entities;

public class PaymentType
{
    public int PaymentTypeId { get; set; }
    public string PaymentTypeName { get; set; } = string.Empty;
    public decimal TipLevyPercentage { get; set; }
    public decimal DiscountPercentage { get; set; }
    public ICollection<OutletPaymentType>? OutletPaymentTypes { get; set; }
    public bool CanEdit { get; set; }
}