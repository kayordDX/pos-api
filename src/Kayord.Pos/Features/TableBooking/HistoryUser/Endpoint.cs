using Kayord.Pos.Data;
using Kayord.Pos.Features.TableBooking.History;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.HistoryUser
{
    public class Endpoint : Endpoint<Request, List<Response>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/tableBooking/myHistory/{userId}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var booking = _dbContext.TableBooking.Include(i => i.SalesPeriod)
                .Where(x => x.UserId == req.UserId)
                .Where(x => x.SalesPeriod.OutletId == req.OutletId)
                .Where(x => x.CloseDate != null);

            if (req.TableBookingId > 0)
            {
                booking = booking.Where(x => x.Id.ToString().StartsWith(req.TableBookingId.ToString()));
            }

            if (req.CashUpUserId > 0)
            {
                booking = booking.Where(x => x.CashUpUserId == req.CashUpUserId);
            }
            else
            {
                booking = booking.Where(x => x.CashUpUserId == null);
            }

            var result = await booking.OrderByDescending(x => x.CloseDate)
                .ProjectToDto()
                .ToListAsync();

            await SendAsync(result);
        }
    }
}