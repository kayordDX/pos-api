namespace Kayord.Pos.DTO;

public class TableBookingDTO
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public string BookingName { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public DateTime? CloseDate { get; set; }
    public string UserId { get; set; } = string.Empty;
    public UserDTO User { get; set; } = default!;
}