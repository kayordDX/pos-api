using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using YamlDotNet.Core.Tokens;

namespace Kayord.Pos.Features.TableOrder.SendToKitchen
{
    public class Endpoint : Endpoint<Request, Response>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/order/sendKitchen");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var orderItemsToUpdate = await _dbContext.OrderItem
                .Where(oi => oi.OrderItemStatusId == 1 && oi.TableBookingId == req.TableBookingId)
                .ToListAsync();

            foreach (var orderItem in orderItemsToUpdate)
            {
                orderItem.OrderItemStatusId = 2;
            }

            await _dbContext.SaveChangesAsync();
            await SendAsync(new Response { IsSuccess = true });

        }
    }
}