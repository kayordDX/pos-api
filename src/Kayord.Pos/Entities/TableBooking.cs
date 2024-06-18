namespace Kayord.Pos.Entities
{
    public class TableBooking
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; } = default!;
        public string BookingName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public DateTime? CloseDate { get; set; }
        public int SalesPeriodId { get; set; }
        public SalesPeriod SalesPeriod { get; set; } = default!;
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = default!;
        public List<OrderItem>? OrderItems { get; set; }
        public List<Adjustment>? Adjustments { get; set; }
        public int? CashUpUserId { get; set; }
        public decimal? Total { get; set; }
        public CashUpUser? CashUpUser { get; set; }
        public List<Payment>? Payments { get; set; }



    }
}
