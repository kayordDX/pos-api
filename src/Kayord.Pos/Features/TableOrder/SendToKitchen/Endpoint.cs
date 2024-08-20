using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;
using YamlDotNet.Core.Tokens;

namespace Kayord.Pos.Features.TableOrder.SendToKitchen
{
    public class Endpoint : Endpoint<Request, Response>
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
            Post("/order/sendKitchen");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var orderItemsToUpdate = await _dbContext.OrderItem
                .Where(oi => oi.OrderItemStatusId == 1 && oi.TableBookingId == req.TableBookingId)
                .ToListAsync();

            OrderGroup order = new();
            await _dbContext.OrderGroup.AddAsync(order);

            foreach (var orderItem in orderItemsToUpdate)
            {
                orderItem.OrderItemStatusId = 2;
                orderItem.OrderUpdated = DateTime.UtcNow;
                orderItem.OrderGroup = order;
            }

            await _dbContext.SaveChangesAsync();

            await PublishAsync(new NotificationEvent()
            {
                UserId = _cu.UserId ?? "",
                Title = "New Order",
                Body = "New order received",
            }, Mode.WaitForNone);
            await SendAsync(new Response { IsSuccess = true });
        }
    }
}