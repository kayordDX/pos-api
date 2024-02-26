
using Kayord.Pos.Entities;
using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.SalesPeriod.CashUp;

public class TableCashUp
{
    public List<BillOrderItemDTO> OrderItems { get; set; } = new List<BillOrderItemDTO>();
    public decimal Total { get; set; } = 0;
    public List<Payment> PaymentsReceived { get; set; } = new List<Payment>();
    public decimal TablePaymentTotal { get; set; } = 0;
    public decimal Balance { get; set; } = 0;
    public string? UserId { get; set; }
    public UserDTO? User { get; set; }


}