using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.TableOrder.GetBill;

public class Response
{
    public List<BillOrderItemDTO> OrderItems { get; set; } = new List<BillOrderItemDTO>();
    public List<BillOrderItemDTO> SummaryOrderItems { get; set; } = new List<BillOrderItemDTO>();
    public List<BillCategoryDTO> BillCategories { get; set; } = new List<BillCategoryDTO>();
    public decimal Total { get; set; } = 0;
    public decimal TotalExVAT { get; set; } = 0;
    public decimal VAT { get; set; } = 0;

    public List<Payment> PaymentsReceived { get; set; } = new List<Payment>();
    public decimal Balance { get; set; } = 0;
    public decimal TipAmount { get; set; } = 0;
    public DateTime BillDate { get; set; }
    public List<Entities.Adjustment>? Adjustments { get; set; }
    public bool IsCashedUp { get; set; }
    public string? TableName { get; set; }
    public string? Waiter { get; set; }
    public bool IsClosed { get; set; }
}

public class TableTotal
{
    public decimal Total { get; set; }
    public decimal TotalPayments { get; set; }
    public decimal TipTotal { get; set; }

}