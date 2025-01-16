namespace Kayord.Pos.Features.TableOrder.UpdateGroupOrder
{
    public class Request
    {
        public int OrderGroupId { get; set; }
        public int OrderItemStatusId { get; set; }
        public string? RoleIds { get; set; }
    }
}