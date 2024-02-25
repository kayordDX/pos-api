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
            if (_user.UserId == null)
            {
                await SendForbiddenAsync();
                return;
            }

            Entities.TableBooking entity = new()
            {
                TableId = req.TableId,
                BookingName = req.BookingName,
                SalesPeriodId = req.SalesPeriodId,
                UserId = _user.UserId
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