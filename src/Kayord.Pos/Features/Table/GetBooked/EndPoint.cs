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
          
            if(req.myBooking)
            {
                //current bookings
            var results = await _dbContext.TableBooking
                .Where(booking => booking.Table.Section.OutletId == req.OutletId && booking.StaffId == _cu.Id &&
                                  _dbContext.TableCashUp.All(cashUp => cashUp.TableBookingId != booking.Id))
                .Where(x => x.Table.Section.OutletId == req.OutletId)
                .ProjectToDto()
                .ToListAsync();

            await SendAsync(results);
            }
            else{
               var results = await _dbContext.TableBooking
                .Where(booking => booking.Table.Section.OutletId == req.OutletId && booking.StaffId != _cu.Id &&
                                  _dbContext.TableCashUp.All(cashUp => cashUp.TableBookingId != booking.Id))
                .Where(x => x.Table.Section.OutletId == req.OutletId)
                .ProjectToDto()
                .ToListAsync();

            await SendAsync(results);

            }
            await SendAsync(new List<Response>());
            
        }
    }
}