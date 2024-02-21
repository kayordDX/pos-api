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
            OrderItem? entity = await _dbContext.OrderItem.FindAsync(req.OrderItemId);
            OrderItemStatus? oIS = await _dbContext.OrderItemStatus.FirstOrDefaultAsync(x => x.OrderItemStatusId == req.OrderItemStatusId);

            if (entity != null && oIS != null)
            {
                entity.OrderItemStatusId = req.OrderItemStatusId;
                if (oIS.isComplete)
                    entity.OrderCompleted = DateTime.Now;

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