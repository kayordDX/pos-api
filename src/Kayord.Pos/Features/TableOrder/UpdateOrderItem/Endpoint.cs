using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            var Notification = "";
            var TableName = "";
            var nC = 0;
            string tUserId = "";
            var Status = "";
            EntityEntry<OrderGroup>? groupEntity = null;
            OrderItemStatus? oIS = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);
            if (oIS != null && oIS.assignGroup)
            {
                OrderGroup order = new();
                groupEntity = await _dbContext.OrderGroup.AddAsync(order);
            }
            foreach (int r in req.OrderItemIds)
            {
                OrderItem? entity = await _dbContext.OrderItem.
                    Include(x => x.TableBooking)
                    .ThenInclude(x => x.Table)
                    .FirstOrDefaultAsync(x => x.OrderItemId == r);

                if (entity != null && oIS != null)
                {
                    Status = oIS.Status;
                    entity.OrderItemStatusId = req.OrderItemStatusId;
                    entity.OrderGroup = groupEntity?.Entity;
                    entity.OrderUpdated = DateTime.UtcNow;
                    if (oIS.isComplete)
                        entity.OrderCompleted = DateTime.Now;
                    if (oIS.Notify)
                    {
                        MenuItem? i = await _dbContext.MenuItem.FirstOrDefaultAsync(x => x.MenuItemId == entity.MenuItemId);
                        if (i != null)
                        {
                            nC++;
                            TableName = entity.TableBooking.Table.Name;
                            Notification = Notification == "" ? i.Name : Notification + ", " + i.Name;
                            tUserId = entity.TableBooking.UserId;
                        }
                    }
                }
                else
                {
                    await SendAsync(new Response() { IsSuccess = false });
                }
            }
            if (Notification != "")
            {
                await PublishAsync(new SignalEvent()
                {
                    UserId = tUserId,
                    Notification = Notification + " - " + Status,
                    DateSent = DateTime.Now,
                    DateExpires = DateTime.Now.AddMinutes(30)
                }, Mode.WaitForNone);

                await PublishAsync(new NotificationEvent()
                {
                    UserId = tUserId,
                    Title = "Test",
                    Body = Notification + " - " + Status,
                }, Mode.WaitForNone);
            }
            await _dbContext.SaveChangesAsync();
            await SendAsync(new Response() { IsSuccess = true });


        }
    }
}