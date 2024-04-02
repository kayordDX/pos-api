using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.GetHistory
{
    public class Endpoint : EndpointWithoutRequest<List<Response>>
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
            Get("/tableBooking/myHistory");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _dbContext.TableBooking
                .Where(x => x.UserId == _user.UserId)
                .Where(x => x.CloseDate != null)
                .OrderByDescending(x => x.CloseDate)
                .ProjectToDto()
                .ToListAsync();

            await SendAsync(result);
        }
    }
}