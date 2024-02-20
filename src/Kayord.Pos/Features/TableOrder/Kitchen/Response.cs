using Kayord.Pos.DTO;

namespace Kayord.Pos.Features.Kitchen.GetOrders;

public class Response
{
    public int TableBookingId {get;set;}
    public string TableName {get;set;} = string.Empty;
    public List<BillOrderItemDTO> OrderItems { get; set; } = new List<BillOrderItemDTO>();
}