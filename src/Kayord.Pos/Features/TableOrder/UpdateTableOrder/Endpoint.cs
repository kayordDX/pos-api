using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Entities;
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
            var updateableStatus = await _dbContext.OrderItemStatus.Where(x => x.isBillable == true && x.isComplete == false).Select(rd => rd.OrderItemStatusId).ToListAsync();
            var ois = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);
            Entities.Table table = new();
            bool notify = true;
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
                    if (ois.isComplete)
                        i.OrderCompleted = DateTime.Now;
                    if (ois.Notify && table.TableId == i.TableBooking.TableId && notify)
                    {
                        _dbContext.Add(new UserNotification()
                        {
                            UserId = i.TableBooking.UserId,
                            Notification = table.Name + "- All Orders - " + ois.Status,
                            DateSent = DateTime.Now,
                            DateExpires = DateTime.Now.AddMinutes(30)
                        });
                        notify = false;
                    }
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