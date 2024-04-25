using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.UpdateGroupOrder
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
            Post("/order/updateOrderGroup");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            OrderItemStatus? orderItemStatus = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);

            var orderItems = await _dbContext.OrderItem
                .Include(x => x.TableBooking)
                    .ThenInclude(b => b.Table)
                .Where(x => x.OrderGroupId == req.OrderGroupId)
                .ToListAsync(ct);

            bool notify = orderItemStatus?.Notify ?? false;
            NotificationEvent notification = new();
            foreach (var item in orderItems)
            {
                notification.Title = "Order ready";
                notification.Body = $"Order #{req.OrderGroupId} - {item.TableBooking.Table.Name}";
                notification.UserId = item.TableBooking.UserId;
                item.OrderItemStatusId = req.OrderItemStatusId;
            }

            if (notify)
            {
                await PublishAsync(notification, Mode.WaitForNone);
            }

            await _dbContext.SaveChangesAsync();
            await SendAsync(new Response() { IsSuccess = true });
        }
    }
}