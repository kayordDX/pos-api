using Humanizer;
using Kayord.Pos.DTO;


namespace Kayord.Pos.Features.Order.BackOffice;
public class OrderItemStatusDTO
{
    public int OrderItemStatusId { get; set; }
    public string Status { get; set; } = string.Empty;
}