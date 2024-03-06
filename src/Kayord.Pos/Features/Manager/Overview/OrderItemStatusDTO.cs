using Humanizer;
using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.Manager.OrderView;
public class OrderItemStatusDTO
{
    public int OrderItemStatusId { get; set; }
    public string Status { get; set; } = string.Empty;
}