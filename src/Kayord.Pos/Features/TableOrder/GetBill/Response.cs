using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.TableOrder.GetBill;

public class Response
{
    public List<BillOrderItemDTO> OrderItems { get; set; } = new List<BillOrderItemDTO>();
    public decimal Total { get; set; } = 0;
}