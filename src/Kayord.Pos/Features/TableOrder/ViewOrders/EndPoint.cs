using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Order.ViewOrders
{
    public class Endpoint : Endpoint<Request, List<Pos.Entities.TableOrder>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/order/table/{tableId:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var orders = await _dbContext.TableOrder
                .Where(order => order.TableBookingId == req.TableBookingId)
                .ToListAsync();

            await SendAsync(orders);
        }
    }
}