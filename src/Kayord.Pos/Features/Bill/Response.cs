using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.Bill;

public class Response
{
    public List<BillOrderItemDTO> OrderItems { get; set; } = [];
    // public List<BillItem> BillItems { get; set; } = [];
    public decimal Total { get; set; } = 0;
    public decimal TotalExVAT { get; set; } = 0;
    public decimal VAT { get; set; } = 0;

    public List<Payment> PaymentsReceived { get; set; } = [];
    public decimal Balance { get; set; } = 0;
    public decimal TipAmount { get; set; } = 0;
    public DateTime BillDate { get; set; }
    public List<Entities.Adjustment>? Adjustments { get; set; }
    public bool IsCashedUp { get; set; }
}

public class TableTotal
{
    public decimal Total { get; set; }
    public decimal TotalPayments { get; set; }
    public decimal TipTotal { get; set; }

}