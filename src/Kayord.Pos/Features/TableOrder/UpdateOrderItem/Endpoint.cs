using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Events;
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
            var Notification = "";
            var TableName = "";
            var nC = 0;
            string tUserId = "";
            var Status = "";
            OrderItemStatus? oIS = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);
            OrderGroup order = new();
            if (oIS != null && oIS.assignGroup)
            {
                await _dbContext.OrderGroup.AddAsync(order);
            }

            List<int>? divisions = new();
            int outletId = 0;
            bool soundNotify = false;

            foreach (int r in req.OrderItemIds)
            {
                OrderItem? entity = await _dbContext.OrderItem
                    .Include(x => x.TableBooking)
                        .ThenInclude(x => x.SalesPeriod)
                    .Include(x => x.TableBooking)
                        .ThenInclude(x => x.Table)
                    .Include(x => x.MenuItem)
                    .FirstOrDefaultAsync(x => x.OrderItemId == r);

                if (entity != null && oIS != null)
                {
                    Status = oIS.Status;
                    entity.OrderItemStatusId = req.OrderItemStatusId;
                    if (oIS != null && oIS.assignGroup)
                    {
                        entity.OrderGroup = order;
                    }
                    entity.OrderUpdated = DateTime.UtcNow;
                    if (oIS?.isComplete ?? false)
                        entity.OrderCompleted = DateTime.Now;
                    if (oIS?.Notify ?? false)
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

                    if (oIS?.isBackOffice ?? false)
                    {
                        outletId = entity.TableBooking.SalesPeriod.OutletId;
                        var divisionId = entity.MenuItem.DivisionId ?? 0;
                        if (!divisions.Contains(divisionId))
                        {
                            divisions.Add(divisionId);
                        }
                        soundNotify = true;
                    }
                }
                else
                {
                    await SendAsync(new Response() { IsSuccess = false });
                }
            }
            if (Notification != "")
            {
                await PublishAsync(new NotificationEvent()
                {
                    UserId = tUserId,
                    Title = "Item Status",
                    Body = TableName + " - " + Notification + " - " + Status,
                }, Mode.WaitForNone);
            }

            if (soundNotify)
            {
                await PublishAsync(new SoundEvent() { OutletId = outletId, Divisions = divisions });
            }

            await _dbContext.SaveChangesAsync();
            await SendAsync(new Response() { IsSuccess = true });
        }
    }
}