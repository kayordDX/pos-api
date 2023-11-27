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
        public int StaffId { get; set; }
        public Staff Staff { get; set; } = default!;
    }
}
