public class OrderItemStatusAudit
{
    public long Id { get; set; }
    public int OrderItemId { get; set; }
    public int OrderItemStatusId { get; set; }
    public DateTime StatusDate { get; set; }
    public string UserId { get; set; } = string.Empty;
}