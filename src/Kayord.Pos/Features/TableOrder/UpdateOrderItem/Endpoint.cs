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
            if (oIS == null)
            {
                throw new Exception("Status not found");
            }
            OrderGroup order = new();
            if (oIS.AssignGroup)
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

                if (entity != null)
                {

                    // If send to kitchen add extra validation
                    if (req.OrderItemStatusId == 2)
                    {
                        if (entity.TableBooking.CloseDate != null)
                        {
                            throw new Exception("Table is closed");
                        }
                    }

                    Status = oIS.Status;
                    entity.OrderItemStatusId = req.OrderItemStatusId;
                    if (oIS.AssignGroup)
                    {
                        entity.OrderGroup = order;
                    }
                    entity.OrderUpdated = DateTime.UtcNow;
                    if (oIS?.IsComplete ?? false)
                        entity.OrderCompleted = DateTime.Now;
                    if (oIS?.IsNotify ?? false)
                    {
                        Entities.MenuItem? i = await _dbContext.MenuItem.FirstOrDefaultAsync(x => x.MenuItemId == entity.MenuItemId);
                        if (i != null)
                        {
                            nC++;
                            TableName = entity.TableBooking.Table.Name;
                            Notification = Notification == "" ? i.Name : Notification + ", " + i.Name;
                            tUserId = entity.TableBooking.UserId;
                        }
                    }

                    if (oIS?.IsBackOffice ?? false)
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

            // Stock
            if (oIS?.IsUpdateStock ?? false)
            {
                // stock event publish
                await PublishAsync(new StockEvent() { OrderItemIds = req.OrderItemIds }, Mode.WaitForNone);
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
                var roleIds = _dbContext.RoleDivision.Where(x => divisions.Contains(x.DivisionId)).Select(x => x.RoleId).ToList();
                await PublishAsync(new SoundEvent() { OutletId = outletId, RoleIds = roleIds });
            }

            await _dbContext.SaveChangesAsync();
            await SendAsync(new Response() { IsSuccess = true });
        }
    }
}