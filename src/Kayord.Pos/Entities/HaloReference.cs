namespace Kayord.Pos.Entities;

public class HaloReference
{
    public Guid Id { get; set; }
    public string? HaloRef { get; set; }
    public int TableBookingId { get; set; }
    public string UserId { get; set; } = string.Empty;
}