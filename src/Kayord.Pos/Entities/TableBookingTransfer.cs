namespace Kayord.Pos.Entities;

public class TableBookingTransfer
{
    public int Id { get; set; }
    public string FromUserId { get; set; } = string.Empty;
    public string ToUserId { get; set; } = string.Empty;
    public DateTime? TransferDate { get; set; }
}