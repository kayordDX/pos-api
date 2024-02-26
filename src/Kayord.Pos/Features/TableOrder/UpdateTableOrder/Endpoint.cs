using Kayord.Pos.Data;
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
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var updateableStatus = await _dbContext.OrderItemStatus.Where(x => x.isBillable == true && x.isComplete == false).Select(rd => rd.OrderItemStatusId).ToListAsync();
            var ois = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);
            if (updateableStatus != null && ois != null)
            {
                List<OrderItem>? entities = await _dbContext.OrderItem.Where(x => updateableStatus.Contains(x.OrderItemStatusId) && x.TableBookingId == req.TableBookingId).ToListAsync();
                foreach (OrderItem i in entities)
                {
                    i.OrderItemStatusId = req.OrderItemStatusId;
                    i.OrderUpdated = DateTime.UtcNow;

                    if (ois.isComplete)
                        i.OrderCompleted = DateTime.Now;
                    if (ois.Notify)
                        _dbContext.Add(new UserNotification()
                        {
                            UserId = i.TableBooking.UserId,
                            Notification = i.TableBooking.Table.Name + "- All Orders - " + ois.Status,
                            DateSent = DateTime.Now,
                            DateExpires = DateTime.Now.AddMinutes(30)
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