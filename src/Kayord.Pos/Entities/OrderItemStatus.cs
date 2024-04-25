namespace Kayord.Pos.Entities;
public class OrderItemStatus
{
    public int OrderItemStatusId { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool assignGroup { get; set; } = false;
    public bool isFrontLine { get; set; } = false;
    public bool isBackOffice { get; set; } = false;
    public bool isComplete { get; set; } = false;
    public bool isCancelled { get; set; } = false;
    public bool isBillable { get; set; } = true;
    public bool Notify { get; set; } = false;
    public int Priority { get; set; } = 0;
}