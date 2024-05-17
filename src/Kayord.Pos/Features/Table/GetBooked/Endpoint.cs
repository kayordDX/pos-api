using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Table.GetMyBooked
{
    public class Endpoint : Endpoint<Request, List<Response>>
    {
        private readonly AppDbContext _dbContext;
        private readonly CurrentUserService _cu;

        public Endpoint(AppDbContext dbContext, CurrentUserService cu)
        {
            _dbContext = dbContext;
            _cu = cu;
        }

        public override void Configure()
        {
            Get("/table/booked");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            List<Response> results = new();
            if (req.myBooking)
            {
                //current bookings
                results = await _dbContext.TableBooking
                    .Where(booking => booking.Table.Section.OutletId == req.OutletId && booking.UserId == _cu.UserId &&
                                      booking.CloseDate == null)
                    .Where(x => x.Table.Section.OutletId == req.OutletId)
                    .OrderBy(x => x.Table.Position)
                    .ProjectToDto()
                    .ToListAsync();
            }
            else
            {
                results = await _dbContext.TableBooking
                    .Where(booking =>
                        booking.Table.Section.OutletId == req.OutletId &&
                        booking.UserId != _cu.UserId &&
                        booking.CloseDate == null
                    )
                    .Where(x => x.Table.Section.OutletId == req.OutletId)
                    .OrderBy(x => x.Table.Position)
                    .ProjectToDto()
                    .ToListAsync();
            }
            await SendAsync(results);
        }
    }
}