using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.Kitchen.GetOrders;

public class Response
{
    public List<BillOrderItemDTO> OrderItems { get; set; } = new List<BillOrderItemDTO>();
}