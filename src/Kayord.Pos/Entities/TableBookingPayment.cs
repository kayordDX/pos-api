namespace Kayord.Pos.Entities
{
    public class TableBookingPayment
    {
        public int Id { get; set; }
        public int TableBookingId { get; set; }
        public TableBooking TableBooking { get; set; } = default!;
        // public List<TableBookingPayment> TableBookingPayments { get; set; } = new List<TableBookingPayment>();

    }
}
