using Kayord.Pos.DTO;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.TableOrder.GetBill;

public class Response
{
    public List<BillOrderItemDTO> OrderItems { get; set; } = new List<BillOrderItemDTO>();
    public decimal Total { get; set; } = 0;
    public decimal TotalExVAT { get; set; } = 0;
    public decimal VAT { get; set; } = 0;

    public List<Payment> PaymentsReceived { get; set; } = new List<Payment>();
    public decimal Balance { get; set; } = 0;
    public decimal TipAmount { get; set; } = 0;
    public DateTime BillDate { get; set; }
}