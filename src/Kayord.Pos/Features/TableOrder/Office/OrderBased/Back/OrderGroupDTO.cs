using Humanizer;
using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.Order.BackOffice;
public class OrderGroupDTO
{
    public int OrderGroupId { get; set; }

    public List<OrderItemDTO>? OrderItems { get; set; }

}