namespace Kayord.Pos.Entities;

public class TableBookingTransfer
{
    public int Id { get; set; }
    public int TableBookingId { get; set; }
    public string FromUserId { get; set; } = string.Empty;
    public string ToUserId { get; set; } = string.Empty;
    public string ByUserId { get; set; } = string.Empty;
    public DateTime TransferDate { get; set; } = DateTime.Now;
}