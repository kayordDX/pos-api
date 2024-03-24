using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.Get
{
    public class Endpoint : Endpoint<Request, Response>
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
            Get("/tableBooking/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var result = await _dbContext.TableBooking
                .Where(x => x.Id == req.Id)
                .ProjectToDto()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(result);
        }
    }
}