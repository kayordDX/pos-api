using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.UpdateOrderItem
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
            Post("/order/updateOrderItem");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            OrderItem? entity = await _dbContext.OrderItem.
            Include(x => x.TableBooking)
            .ThenInclude(x => x.Table)
            .FirstOrDefaultAsync(x => x.OrderItemId == req.OrderItemId);
            OrderItemStatus? oIS = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);

            if (entity != null && oIS != null)
            {
                entity.OrderItemStatusId = req.OrderItemStatusId;
                if (oIS.isComplete)
                    entity.OrderCompleted = DateTime.Now;
                if (oIS.Notify)
                {
                    MenuItem? i = await _dbContext.MenuItem.FirstOrDefaultAsync(x => x.MenuItemId == entity.MenuItemId);
                    if (i != null)
                        _dbContext.Add(new UserNotification()
                        {
                            UserId = entity.TableBooking.UserId,
                            Notification = entity.TableBooking.Table.Name + " - " + i.Name + " - " + oIS.Status,
                            DateSent = DateTime.Now,
                        });
                }
                await _dbContext.SaveChangesAsync();
                await SendAsync(new Response() { IsSuccess = true });

            }
            else
            {
                await SendAsync(new Response() { IsSuccess = false });
            }
        }
    }
}