using Kayord.Pos.Data;

namespace Kayord.Pos.Features.TableOrder.Create
{
    public class Endpoint : Endpoint<Request, Pos.Entities.TableOrder>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/order");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            Pos.Entities.TableOrder entity = new Pos.Entities.TableOrder()
            {
                TableBookingId = req.TableBookingId,
            };

            await _dbContext.TableOrder.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.TableOrder.FindAsync(entity.TableOrderId);
            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(result);
        }
    }
}