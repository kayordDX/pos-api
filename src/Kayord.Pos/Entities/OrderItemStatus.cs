namespace Kayord.Pos.Entities;
public class OrderItemStatus
{
    public int OrderItemStatusId { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool isFrontLine {get;set;} = false;
    public bool isKitchen {get;set;} = false;

}