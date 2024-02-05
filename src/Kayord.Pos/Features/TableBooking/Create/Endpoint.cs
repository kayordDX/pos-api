using Kayord.Pos.Data;
using Kayord.Pos.Services;

namespace Kayord.Pos.Features.TableBooking.Create
{
    public class Endpoint : Endpoint<Request, Pos.Entities.TableBooking>
    {
        private readonly AppDbContext _dbContext;
        private readonly CurrentUserService _user;

        public Endpoint(AppDbContext dbContext, CurrentUserService user)
        {
            _dbContext = dbContext;
            _user = user;
        }

        public override void Configure()
        {
            Post("/tableBooking");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            Pos.Entities.TableBooking entity = new Entities.TableBooking()
            {
                TableId = req.TableId,
                BookingName = req.BookingName,
                SalesPeriodId = req.SalesPeriodId,
                UserId = _user.UserId
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