using Kayord.Pos.Data;

namespace Kayord.Pos.Features.TableBooking.Create
{
    public class Endpoint : Endpoint<Request, Pos.Entities.TableBooking>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/tableBooking");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            Pos.Entities.TableBooking entity = new Pos.Entities.TableBooking()
            {
                TableId = req.TableId,
                BookingName = req.BookingName,
                SalesPeriodId = req.SalesPeriodId,
                StaffId = req.StaffId,
            };


            var tableCashUp = new Pos.Entities.TableCashUp
            {
                TableBookingId = entity.Id
            };



            await _dbContext.TableBooking.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.TableBooking.FindAsync(entity.Id);
            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(result);
        }
    }
}