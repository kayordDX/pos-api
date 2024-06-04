namespace Kayord.Pos.Entities;

public class OutletPaymentType
{
    public int PaymentTypeId { get; set; }
    public PaymentType PaymentType { get; set; } = default!;
    public int OutletId { get; set; }
    public Outlet Outlet { get; set; } = default!;

}