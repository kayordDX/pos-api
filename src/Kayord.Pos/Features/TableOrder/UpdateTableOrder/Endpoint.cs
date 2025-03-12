using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableOrder.UpdateTableOrder
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
            Post("/order/updateTableOrder");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var updateableStatus = await _dbContext.OrderItemStatus.Where(x => x.IsBillable == true && x.IsComplete == false).Select(rd => rd.OrderItemStatusId).ToListAsync();
            var ois = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);
            Entities.Table table = new();
            bool notify = true;
            NotificationEvent notification = new();
            if (updateableStatus != null && ois != null)
            {
                List<OrderItemDTO>? entities = await _dbContext.OrderItem.Where(x => updateableStatus.Contains(x.OrderItemStatusId) && x.TableBookingId == req.TableBookingId).ProjectToDto().ToListAsync();
                foreach (OrderItemDTO i in entities)
                {
                    OrderItem oi = await _dbContext.OrderItem.FindAsync(i.OrderItemId) ?? new();
                    oi.OrderItemStatusId = req.OrderItemStatusId;
                    oi.OrderUpdated = DateTime.UtcNow;
                    if (table.TableId != i.TableBooking.TableId)
                        table = await _dbContext.Table.FindAsync(i.TableBooking.TableId) ?? new();
                    if (ois.IsComplete)
                        i.OrderCompleted = DateTime.Now;
                    if (ois.IsNotify && table.TableId == i.TableBooking.TableId && notify)
                    {
                        notification.UserId = i.TableBooking.UserId;
                        notification.Title = "Order Update";
                        notification.Body = table.Name + "- All Orders - " + ois.Status;
                        notify = false;
                    }
                }
                await _dbContext.SaveChangesAsync();
                if (notify)
                {
                    await PublishAsync(notification, Mode.WaitForNone);
                }
                await SendAsync(new Response() { IsSuccess = true });
            }
            else
            {
                await SendAsync(new Response() { IsSuccess = false });
            }
        }
    }
}