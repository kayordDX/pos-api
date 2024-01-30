using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableCashUp.ViewTableCashUps
{
    public class Endpoint : Endpoint<Request, List<Pos.Entities.TableCashUp>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/tableCashUp/tableBooking/{tableBookingId:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var tableCashUps = await _dbContext.TableCashUp
                .Where(cashUp => cashUp.TableBookingId == req.TableBookingId)
                .ToListAsync();

            await SendAsync(tableCashUps);
        }
    }
}