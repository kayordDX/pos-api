namespace Kayord.Pos.Entities;
public class OrderItemStatus
{
    public int OrderItemStatusId { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool AssignGroup { get; set; }
    public bool IsFrontLine { get; set; }
    public bool IsBackOffice { get; set; }
    public bool IsComplete { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsBillable { get; set; } = true;
    public bool IsHistory { get; set; }
    public bool IsNotify { get; set; }
    public int Priority { get; set; }
    public bool IsUpdateStock { get; set; }
}