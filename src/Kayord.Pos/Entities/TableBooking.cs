namespace Kayord.Pos.Entities
{
    public class TableBooking
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; } = default!;
        public string BookingName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public int SalesPeriodId { get; set; }
        public SalesPeriod SalesPeriod { get; set; } = default!;
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = default!;
    }
}
