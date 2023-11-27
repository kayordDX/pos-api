namespace Kayord.Pos.Entities
{
    public class TableCashUp
    {
        public int Id { get; set; }
        public int TableBookingId { get; set; }
        public TableBooking TableBooking { get; set; } = default!;
        public decimal SalesAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CashUpDate { get; set; }
        public int OutletId { get; set; }
        public Outlet Outlet { get; set; } = default!;

    }
}