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
            var result = await _dbContext.TableBooking
                .Where(x => x.UserId == req.UserId)
                .Where(x => x.CloseDate != null)
                .Where(x => x.CashUpUserId == null)
                .OrderByDescending(x => x.CloseDate)
                .ProjectToDto()
                .ToListAsync();

            await SendAsync(result);
        }
    }
}